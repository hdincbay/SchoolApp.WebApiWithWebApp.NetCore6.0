using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Entities.Models;

namespace SchoolApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(string name)
        {
            try
            {
                var result = await _roleManager.CreateAsync(new AppRole { Name = name, ConcurrencyStamp = DateTime.Now.ToString()});
                if (result.Succeeded)
                    return Ok("Rol oluşturuldu.");
                return BadRequest(string.Format("Rol oluşturma işlemi başarısız oldu: {0}", result.Errors.ToString()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                var rolelist = await _roleManager.Roles.ToListAsync();
                return Ok(rolelist);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
