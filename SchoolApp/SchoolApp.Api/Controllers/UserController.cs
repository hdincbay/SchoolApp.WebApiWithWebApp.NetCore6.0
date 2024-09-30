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
        private readonly SignInManager<AppUser> _signInManager;

        public UserController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
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
        [HttpGet("GetOneUser")]
        public async Task<IActionResult> GetOneUser(string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(string userName, string password, string email)
        {
            try
            {
                var user = await _userManager.GetUserNameAsync(new AppUser() { UserName = userName });
                var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (result.Succeeded)
                    return Ok("Login işlemi başarılı.");
                else
                    return BadRequest("Login işlemi başarısız!");
                
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
