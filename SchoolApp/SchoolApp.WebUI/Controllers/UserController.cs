using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SchoolApp.WebUI.ViewModels;
using System.Diagnostics.Eventing.Reader;

namespace SchoolApp.WebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromForm]User user)
        {
            try
            {

                var apiEndpoint = _configuration["apiEndpointAddress"]?.ToString();
                var endpoint = string.Format("{0}/api/User/Login?userName={1}&password={2}&email={3}", apiEndpoint, user.UserName, user.Password, user.Email);
                var client = new RestClient();
                var request = new RestRequest(endpoint, Method.Post);
                var result = await client.ExecuteAsync(request);
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Lesson");
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", ex.Message);
            }
        }
    }
}
