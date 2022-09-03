using LandLeaser.API.Data;
using LandLeaser.API.Data.Models;
using LandLeaser.API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        
        
        //ok if user exists and password password is correct else return unauthorized
        public async Task<IActionResult> Login([FromBody] LoginVM loginVM)
        {
            //Check if the modelstate is valid
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide all the required fields");
            }

            //Check if the user exists in the database
            var userExists = await _userManager.FindByEmailAsync(loginVM.EmailAddress);

            if (userExists is not null && await _userManager.CheckPasswordAsync(userExists, loginVM.Password))
            {
                return Ok("Login successful");
            }
            return Unauthorized();
        }
    }    
}
