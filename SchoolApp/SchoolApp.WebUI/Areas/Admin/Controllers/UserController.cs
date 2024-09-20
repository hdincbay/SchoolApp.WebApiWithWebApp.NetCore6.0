using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;
using SchoolApp.WebUI.Areas.Admin.ViewModels;

namespace SchoolApp.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", ex.ToString());
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] User user)
        {
            try
            {
                var endpoint = string.Format("https://localhost:7081/api/User/Create?userName={0}&email={1}&phoneNumber={2}&password={3}", user.UserName, user.Email, user.PhoneNumber, user.Password);
                var client = new RestClient();
                var request = new RestRequest(endpoint, Method.Post);
                var response = await client.ExecuteAsync(request);
                if (response.StatusCode is System.Net.HttpStatusCode.OK)
                    ViewBag.ResponseText = "Kullanıcı Oluşturuldu.";
                else
                    ViewBag.ResponseText = "İşlem Başarısız!";

                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", ex.ToString());
            }
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var endpoint = "https://localhost:7081/api/User/GetAllList";
                var client = new RestClient();
                var request = new RestRequest(endpoint, Method.Get);
                var response = await client.ExecuteAsync(request);
                var userListJArray = Newtonsoft.Json.JsonConvert.DeserializeObject<JArray>(response.Content!);
                var listUserNew = new List<User>();
                foreach (var item in userListJArray!)
                {
                    listUserNew.Add(new User { Email = item["email"]?.ToString(), PhoneNumber = item["phoneNumber"]?.ToString(), UserName = item["userName"]?.ToString() });
                }
                return View(listUserNew);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", ex.ToString());
            }
        }
        [HttpGet]
        public async Task<IActionResult> RoleOperations([FromQuery]string username)
        {
            try
            {
                
                var client = new RestClient();
                var endpoint = string.Format("https://localhost:7081/api/User/GetOneUser?userName={0}", username);
                var request = new RestRequest(endpoint, Method.Get);
                var response = await client.ExecuteAsync(request);
                var userModel = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(response.Content!);
                var endpoint2 = "https://localhost:7081/api/Role/GetAllRoles";
                var request2 = new RestRequest(endpoint2, Method.Get);
                var response2 = await client.ExecuteAsync(request2);
                var roleListJArray = Newtonsoft.Json.JsonConvert.DeserializeObject<JArray>(response2.Content!);
                var roleList = new List<String>();
                ViewBag.RoleListContent = roleList;
                foreach(var item in roleListJArray!)
                {
                    var roleName = item["name"]?.ToString();
                    roleList.Add(roleName!);
                }
                return View(userModel);
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error", "Home", ex.ToString());
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddRoles(User user, string selectedRole)
        {
            try
            {
                var endpoint = string.Format("https://localhost:7081/api/UserRole/AddRolesToUser?userEmail={0}&role={1}", user.Email, selectedRole);
                var client = new RestClient();
                var request = new RestRequest(endpoint, Method.Post);
                var response = await client.ExecuteAsync(request);
                return RedirectToAction("Index", "Student", "Admin");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error", "Home", ex.ToString());
            }
        }
    }
}
