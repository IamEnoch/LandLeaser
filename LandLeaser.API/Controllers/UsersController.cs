using LandLeaser.API.Data;
using LandLeaser.API.Migrations;
using LandLeaser.API.Models;
using LandLeaser.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LandLeaser.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _appDbContext;

        public UsersController(UserManager<ApplicationUser> userManager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
        }
        
        [Authorize]
        [HttpGet("{email}")]
        public async Task<ActionResult<UserBasicInfo>> GetUser(string email)
        {
            //Get user by email
            var response = await _userManager.FindByEmailAsync(email);

            if(response == null)
            {
                return NotFound("User not found");

            }

            //Record the result of the result got
            var userDetails = new UserBasicInfo()
            {
                Email = response.Email,
                PhoneNumber = response.PhoneNumber,
                FullName = response.FirstName + " " + response.LastName
            };

            return Ok(userDetails);
        }
    }
}
