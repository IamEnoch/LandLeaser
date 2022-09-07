using LandLeaser.API.Data;
using LandLeaser.API.Data.Models;
using LandLeaser.API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        public AuthenticationController(UserManager<ApplicationUser> userManager, AppDbContext context, RoleManager<ApplicationRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> Register([FromBody]RegisterVM registerVm)
        {
            //check model state
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide all the required fields");
            }

            //Check if the user exists
            var userExists = await _userManager.FindByEmailAsync(registerVm.EmailAddress);
            if(userExists is not null)
            {
                return BadRequest("User already exists");
            }

            //Create application user
            ApplicationUser applicationUser = new()
            {
                FirstName = registerVm.FirstName,
                LastName = registerVm.LastName,
                UserName = registerVm.FirstName + "" + registerVm.LastName,
                Email = registerVm.EmailAddress,
                PhoneNumber = registerVm.PhoneNumeber,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(applicationUser, registerVm.Password);

            //if user created = ok elser bad request
            if (result.Succeeded)
            {
                return Ok("User created");
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("login-user")]    
        public async Task<IActionResult> Login([FromBody] LoginVM loginVM)
        {
            //Check if the modelstate is valid
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide all the required fields");
            }

            //Check if the user exists in the database
            var userExists = await _userManager.FindByEmailAsync(loginVM.EmailAddress);

            //ok if user exists and password password is correct else return unauthorized
            if (userExists is not null && await _userManager.CheckPasswordAsync(userExists, loginVM.Password))
            {
                //Generate a token
                var tokenValue = await GenerateJWTTokenAsync(userExists);

                return Ok(tokenValue);
            }
            return Unauthorized();
        }

        private async Task<AuthResultVM> GenerateJWTTokenAsync(ApplicationUser user)
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
                expires: DateTime.UtcNow.AddMinutes(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            //Create jwt token
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

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

            var response = new AuthResultVM()
            {
                Token = jwtToken,
                RefreshToken = refreshToken.Token,
                ExpiresAt = token.ValidTo
            };

            return response;
            
        }
        
    }    
}
