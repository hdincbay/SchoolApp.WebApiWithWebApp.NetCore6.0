using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Entities.Models;

namespace SchoolApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UserController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(string userName, string email, string phoneNumber, string password)
        {
            try
            {
                var result = await _userManager.CreateAsync(new AppUser() { UserName = userName, Email = email, PhoneNumber = phoneNumber}, password);
                if(result.Succeeded)
                    return Ok("Kullanıcı Oluşturuldu.");
                return BadRequest(string.Format("Kullanıcı oluşturma işlemi başarısız oldu: {0}", result.Errors.ToString()));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAllList")]
        public async Task<IActionResult> GetAllList()
        {
            try
            {
                var userList = await _userManager.Users.ToListAsync();
                return Ok(userList);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
