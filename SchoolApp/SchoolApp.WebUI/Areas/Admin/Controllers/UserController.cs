using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;
using SchoolApp.WebUI.Areas.Admin.ViewModels;

namespace SchoolApp.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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
                var apiEndpoint = _configuration["apiEndpointAddress"]?.ToString();
                var endpoint = string.Format("{0}/api/User/Create?userName={1}&email={2}&phoneNumber={3}&password={4}", apiEndpoint, user.UserName, user.Email, user.PhoneNumber, user.Password);
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
                var apiEndpoint = _configuration["apiEndpointAddress"]?.ToString();
                var endpoint = string.Format("{0}/api/User/GetAllList", apiEndpoint);
                var client = new RestClient();
                var request = new RestRequest(endpoint, Method.Get);
                var response = await client.ExecuteAsync(request);
                var userListJArray = Newtonsoft.Json.JsonConvert.DeserializeObject<JArray>(response.Content!);
                var listUserNew = new List<User>();
                foreach (var item in userListJArray!)
                {
                    var endpointByUserRole = string.Format("{0}/api/UserRole/GetRolesByUser?id={1}", apiEndpoint, item["id"]?.ToString());
                    var request2 = new RestRequest(endpointByUserRole, Method.Get);
                    var response2 = await client.ExecuteAsync(request2);
                    var roleListJArray = Newtonsoft.Json.JsonConvert.DeserializeObject<JArray>(response2.Content!);
                    var roleList = new List<string>();
                    foreach(var rl in roleListJArray)
                    {
                        roleList.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<string>(Newtonsoft.Json.JsonConvert.SerializeObject(rl))!);
                    }
                    listUserNew.Add(new User { Email = item["email"]?.ToString(), PhoneNumber = item["phoneNumber"]?.ToString(), UserName = item["userName"]?.ToString(), Roles = roleList});
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
                var apiEndpoint = _configuration["apiEndpointAddress"]?.ToString();
                var client = new RestClient();
                var endpoint = string.Format("{0}/api/User/GetOneUser?userName={1}", apiEndpoint, username);
                var request = new RestRequest(endpoint, Method.Get);
                var response = await client.ExecuteAsync(request);
                var userModel = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(response.Content!);
                var endpoint2 = string.Format("{0}/api/Role/GetAllRoles", apiEndpoint);
                var request2 = new RestRequest(endpoint2, Method.Get);
                var response2 = await client.ExecuteAsync(request2);
                var roleListJArray = Newtonsoft.Json.JsonConvert.DeserializeObject<JArray>(response2.Content!);



                var endpointGetUser = string.Format("{0}/api/User/GetOneUser?userName={1}", apiEndpoint, username);
                var requestGetUser = new RestRequest(endpointGetUser, Method.Get);
                var responseGetUser = await client.ExecuteAsync(requestGetUser);
                var itemGetUserJObj = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(responseGetUser.Content!);
                var endpointByUserRole = string.Format("{0}/api/UserRole/GetRolesByUser?id={1}", apiEndpoint, itemGetUserJObj!["id"]?.ToString());
                var requestGetUserRoles = new RestRequest(endpointByUserRole, Method.Get);
                var responseGetUserRoles = await client.ExecuteAsync(requestGetUserRoles);
                var roleListJArrayByUser = Newtonsoft.Json.JsonConvert.DeserializeObject<JArray>(responseGetUserRoles.Content!);


                var roleList = new List<String>();
                
                ViewBag.RoleListContent = roleList;
                foreach(var item in roleListJArrayByUser!)
                {
                    roleList.Add(item!.ToString());
                }
                ViewBag.RoleList = roleList;
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
                var apiEndpoint = _configuration["apiEndpointAddress"]?.ToString();
                var endpoint = string.Format("{0}/api/UserRole/AddRolesToUser?userEmail={1}&role={2}", apiEndpoint, user.Email, selectedRole);
                var client = new RestClient();
                var request = new RestRequest(endpoint, Method.Post);
                var response = await client.ExecuteAsync(request);
                return RedirectToAction("Index", "User", "Admin");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error", "Home", ex.ToString());
            }
        }
    }
}
