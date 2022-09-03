using LandLeaser.API.Data;
using LandLeaser.API.Data.Models;
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
        //check model state
        //Check if the user exists
        //
    }
}
