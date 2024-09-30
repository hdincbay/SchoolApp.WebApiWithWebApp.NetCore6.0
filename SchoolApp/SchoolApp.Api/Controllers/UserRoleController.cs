using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SchoolApp.Entities.Models;
using SchoolApp.Repositories;

namespace SchoolApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RepositoryContext _context;

        public UserRoleController(UserManager<AppUser> userManager, RepositoryContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpPost("AddRolesToUser")]
        public async Task<IActionResult> AddRolesToUser(string userEmail, string role)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                var result = await _userManager.AddToRoleAsync(user, role);
                if(result.Succeeded)
                    return Ok();
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetRolesByUser")]
        public async Task<IActionResult> GetRolesByUser([FromQuery]string id)
        {
            try
            {
                var user = _context.Users.Where(u => u.Id.Equals(id)).FirstOrDefault();
                var roles = await _userManager.GetRolesAsync(user!);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
