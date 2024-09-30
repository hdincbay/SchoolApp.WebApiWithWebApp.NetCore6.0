using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Newtonsoft.Json.Linq;
using RestSharp;
using SchoolApp.WebUI.Areas.Admin.ViewModels;

namespace SchoolApp.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        
        private readonly IConfiguration _configuration;

        public RoleController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var apiEndpoint = _configuration["apiEndpointAddress"]?.ToString();
                var resource = string.Format("{0}/api/Role/GetAllRoles", apiEndpoint);
                var client = new RestClient();
                var request = new RestRequest(resource, Method.Get);
                var response = await client.ExecuteAsync(request);
                var roleListJArray = Newtonsoft.Json.JsonConvert.DeserializeObject<JArray>(response.Content!);
                var roleList = new List<Role>();
                foreach (var item in roleListJArray!)
                {
                    roleList.Add(new Role(){ Name = item["name"]?.ToString()! });
                }

                return View(roleList);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { message = ex.ToString() });
            }
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
        public async Task<IActionResult> Create([FromForm] Role role)
        {
            try
            {
                var apiEndpoint = _configuration["apiEndpointAddress"]?.ToString();
                var resource = string.Format("{0}/api/Role/Create?name={1}", apiEndpoint, role.Name);
                var client = new RestClient();
                var request = new RestRequest(resource, Method.Post);
                var response = await client.ExecuteAsync(request);

                return RedirectToAction("Index", "Role", "Admin");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", ex.ToString());
            }
        }
    }
}
