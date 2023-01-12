using LandLeaser.API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LandLeaser.Shared.DTOs;
using LandLeaser.Shared.Models;
using LandLeaser.API.Models;

namespace LandLeaser.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public AuthenticationController(UserManager<ApplicationUser> userManager,
            AppDbContext context,
            RoleManager<ApplicationRole> roleManager,
            IConfiguration configuration,
            TokenValidationParameters tokenValidationParameters)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
            _configuration = configuration;
            _tokenValidationParameters = tokenValidationParameters;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]Register register)
        {
            //check model state
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide all the required fields");
            }

            //Check if the user exists
            var userExists = await _userManager.FindByEmailAsync(register.EmailAddress);
            if(userExists is not null)
            {
                return BadRequest("User already exists");
            }

            //Create application user
            ApplicationUser applicationUser = new()
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                UserName = register.FirstName + "" + register.LastName,
                Email = register.EmailAddress,
                PhoneNumber = register.PhoneNumeber,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(applicationUser, register.Password);

            //if user created = ok elser bad request
            if (result.Succeeded)
            {
                return Ok("User created");
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("login")]    
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            //Check if the modelstate is valid
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide all the required fields");
            }

            //Check if the user exists in the database
            var userExists = await _userManager.FindByEmailAsync(loginRequest.EmailAddress);

            //ok if user exists and password password is correct else return unauthorized
            if (userExists is not null && await _userManager.CheckPasswordAsync(userExists, loginRequest.Password))
            {
                //Generate a token
                var tokenValue = await GenerateJWTTokenAsync(userExists, null);

                var result = new LoginResultDto()
                {
                    User = new User()
                    {
                        FullName = userExists.FirstName + userExists.LastName,
                        Email = userExists.Email,
                        PhoneNumber = userExists.PhoneNumber,
                        Id = userExists.Id.ToString(),
                    },
                    LoginResult = new LoginResult()
                    {
                        ExpiresAt = tokenValue.ExpiresAt,
                        RefreshToken = tokenValue.RefreshToken,
                        Token = tokenValue.Token
                    }
                };

                return Ok(result);
            }
            return Unauthorized();
        }

        /// <summary>
        /// Refresh token controller
        /// </summary>
        /// <param name="TokenRequstVM"></param>
        /// <returns></returns>
        /// 
        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
        {
            //Check if the model state is valid
            if (!ModelState.IsValid)
            {
                return BadRequest("Enter all the required fields");
            }

            //Refresh the access token
            var result = await VerifyAndGenerateTokenAsync(tokenRequest);

            return Ok(result);

        }

        private async Task<LoginResult> VerifyAndGenerateTokenAsync(TokenRequest tokenRequest)
        {
            //Create an instance of the token handler
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            //Retrieving the refresh token details
            var storedToken = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequest.RefreshToken);

            //Getting the user associated with the refresh token
            var dbUser = await _userManager.FindByIdAsync(storedToken.UserId.ToString());

            try
            {
                //Validated the access token
                var tokenCheckResult = jwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParameters,
                    out var validatedToken);

                return await GenerateJWTTokenAsync(dbUser, storedToken);

            }
            catch (SecurityTokenExpiredException)
            {
                //Check if the security token is expired
                if (storedToken.DateExpire >= DateTime.UtcNow) 
                {
                    return await GenerateJWTTokenAsync(dbUser, storedToken);
                }
                else
                {
                    return await GenerateJWTTokenAsync(dbUser, null);
                }
            }
        }

        private async Task<LoginResult> GenerateJWTTokenAsync(ApplicationUser user, RefreshToken rToken)
        {
            //Authentication claims = Claim(Stores info about a subject)
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            };

            //Authentication signing key
            var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            //Create a token
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.UtcNow.AddMinutes(5),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            //Create jwt token
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            if(rToken is not null)
            {
                var refreshTokenRresponse = new LoginResult()
                {
                    Token = jwtToken,
                    RefreshToken = rToken.Token,
                    ExpiresAt = token.ValidTo
                };

                return refreshTokenRresponse;
            }

            //Create a refresh token
            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                IsRevoked = false,
                UserId = user.Id,
                DateAdded = DateTime.UtcNow,
                DateExpire = DateTime.UtcNow.AddMonths(6),
                Token = Guid.NewGuid().ToString() + "-" + Guid.NewGuid().ToString()
            };

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            var response = new LoginResult()
            {
                Token = jwtToken,
                RefreshToken = refreshToken.Token,
                ExpiresAt = token.ValidTo
            };

            return response;
            
        }
        
    }    
}
