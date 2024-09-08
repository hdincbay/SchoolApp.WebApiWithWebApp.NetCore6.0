using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Entities.Models;

namespace SchoolApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public UserRoleController(Microsoft.AspNetCore.Identity.UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> AddRolesToUser(string userEmail, IEnumerable<string> roleList)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                var result = await _userManager.AddToRolesAsync(user, roleList);
                if(result.Succeeded)
                    return Ok();
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
