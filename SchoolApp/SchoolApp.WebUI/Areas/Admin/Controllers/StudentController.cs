﻿using log4net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using SchoolApp.WebUI.Areas.Admin.Models;

namespace SchoolApp.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudentController : Controller
    {
        private readonly ILog log = LogManager.GetLogger(typeof(StudentController));
        private readonly IConfiguration _configuration;

        public StudentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult AddRole(Student student, string selectedRole)
        {
            // Modelin özelliklerine erişim
            Console.WriteLine(student.StudentName);
            Console.WriteLine(student.StudentSurname);

            

            // İşlemler tamamlandıktan sonra yönlendirme veya başka bir işlem
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var apiEndpoint = _configuration["apiEndpointAddress"]?.ToString();
                var resource = string.Format("{0}/api/Student/GetAllStudents", apiEndpoint);
                log.Debug("Öğrenci Listesinin endpointi: " + resource);
                var client = new RestClient(resource);
                log.Debug("Client oluşturuldu.");
                var request = new RestRequest(resource, Method.Get);
                log.Debug("Get methodu ile Request oluşturuldu.");
                var response = await client.ExecuteAsync(request);
                log.Debug("İstek gerçekleştirildi...");
                if (response.IsSuccessStatusCode)
                {
                    log.Debug("İstek başarılı.");
                    var jsonData = response.Content;
                    log.Debug("Gelen Json tipindeki veri: " + jsonData);
                    var values = JsonConvert.DeserializeObject<List<Student>>(jsonData is not null ? jsonData : "");
                    log.Debug("Json tipindeki veri Deserialize edildi ve View'e yönlendirildi.");
                    return View(values);
                }
                else
                {
                    log.Error("İstek başarısız!");
                    log.Error("Error sayfasına yönlendiriliyor.");
                    return RedirectToAction("Error", "Home");
                }
            }
            catch (Exception ex)
            {
                log.Error("Index metodunda hata alındı! Alınan hata: " + ex.Message);
                return RedirectToAction("Error", "Home", ex.Message);
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Student student, IFormFile file)
        {
            try
            {
                var apiEndpoint = _configuration["apiEndpointAddress"]?.ToString();
                var resource = string.Format("{0}/api/Student/CreateStudent", apiEndpoint);
                log.Debug("Gidilecek endpoint: " + resource);
                var client = new RestClient();
                log.Debug("Client oluşturuldu.");
                var request = new RestRequest(resource, Method.Post);


                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    log.Debug("Akış oluşturuluyor...");
                    await file.CopyToAsync(stream);
                    log.Debug("Akış oluşturuldu.");
                }
                student.ImageUrl = string.Concat("/images/", file.FileName);
                var jsonBody = JsonConvert.SerializeObject(student);
                log.Debug("Requestin Json Body'si: " + jsonBody);
                request.AddJsonBody(jsonBody);
                var response = await client.ExecuteAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    log.Error("Öğrenci ekleme işlemi başarılı oldu.");
                    return RedirectToAction("Index", "Student", "Admin");
                }
                else
                {
                    log.Error("Öğrenci ekleme işlemi başarısız oldu.");
                }
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Update([FromRoute] int id, IFormFile file)
        {
            try
            {
                var apiEndpoint = _configuration["apiEndpointAddress"]?.ToString();
                var resource = string.Format("{0}/api/Student/GetOneStudent/{1}", apiEndpoint, id);
                var client = new RestClient();
                var request = new RestRequest(resource, Method.Get);
                var response = await client.ExecuteAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = response.Content;
                    var student = JsonConvert.DeserializeObject<Student>(jsonData is not null ? jsonData : "");
                    if(student is not null)
                    {
                        student.ImageUrl = student.ImageUrl?.Substring(8);
                        return View(student);
                    }
                    
                }
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromForm] Student student, [FromForm] IFormFile file)
        {
            try
            {
                var apiEndpoint = _configuration["apiEndpointAddress"]?.ToString();
                var resource1 = string.Format("{0}/api/Student/GetOneStudent/{1}", apiEndpoint, student.StudentId);
                var client1 = new RestClient();
                var request1 = new RestRequest(resource1, Method.Get);
                var response1 = await client1.ExecuteAsync(request1);
                if (response1.IsSuccessStatusCode)
                {
                    var jsonData1 = response1.Content;
                    var student1 = JsonConvert.DeserializeObject<Student>(jsonData1 is not null ? jsonData1 : "");
                }

                if (student is not null)
                {
                    var resource2 = string.Format("{0}/api/Student/UpdateStudent/{1}", apiEndpoint, student.StudentId);
                    log.Debug("Gidilecek endpoint: " + resource2);
                    var client2 = new RestClient();
                    log.Debug("Client oluşturuldu.");
                    var request2 = new RestRequest(resource2, Method.Post);


                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        log.Debug("Akış oluşturuluyor...");
                        await file.CopyToAsync(stream);
                        log.Debug("Akış oluşturuldu.");
                    }
                    student.ImageUrl = string.Concat("/images/", file.FileName);
                    var jsonBody = JsonConvert.SerializeObject(student);
                    log.Debug("Requestin Json Body'si: " + jsonBody);
                    request2.AddJsonBody(jsonBody);
                    var response2 = await client2.ExecuteAsync(request2);
                    if (response2.IsSuccessStatusCode)
                    {
                        TempData["ServiceResponse"] = "Öğrenci başarıyla güncellendi.";
                    }
                    else
                    {
                        log.Error("Öğrenci güncelleme işlemi başarısız oldu.");
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var apiEndpoint = _configuration["apiEndpointAddress"]?.ToString();
                var resource1 = string.Format("{0}/api/Student/GetOneStudent/{1}", apiEndpoint, id);
                log.Debug("Gidilecek endpoint: " + resource1);
                var client1 = new RestClient();
                log.Debug("Client oluşturuldu.");
                var request1 = new RestRequest(resource1, Method.Get);
                var response1 = await client1.ExecuteAsync(request1);
                if (response1.IsSuccessStatusCode)
                {
                    var jsonData = response1.Content;
                    var student = JsonConvert.DeserializeObject<Student>(jsonData is not null ? jsonData : "");
                    if (student is not null)
                    {
                        log.Debug("student is not null!");
                        var resource2 = string.Format("{0}/api/Student/DeleteStudent/{1}", apiEndpoint, student.StudentId);
                        log.Debug("Gidilecek endpoint: " + resource2);
                        var client2 = new RestClient();
                        var request2 = new RestRequest(resource2, Method.Delete);
                        log.Debug("Gidilecek endpoint: " + resource1);
                        var response2 = await client2.ExecuteAsync(request2);
                        if(response2.IsSuccessStatusCode)
                        {
                            log.Debug("İstek gerçekleştirildi...");
                        }
                    }
                }
                else
                {
                    log.Error("Öğrenci bilgileri getirilemedi!");
                    return View();
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", ex.Message);
            }
            return RedirectToAction("Index", "Student");
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                var apiEndpoint = _configuration["apiEndpointAddress"]?.ToString();
                var endpointByRoles = string.Format("{0}/api/Role/GetAllRoles", apiEndpoint);
                var resource = string.Format("{0}/api/Student/GetOneStudent/{1}", apiEndpoint, id);
                log.Debug("Gidilecek endpoint: " + resource);
                var client = new RestClient();
                log.Debug("Client oluşturuldu.");
                var request = new RestRequest(resource, Method.Get);
                var response = await client.ExecuteAsync(request);
                
                log.Debug("İstek gerçekleştiriliyor...");
                if (response.IsSuccessStatusCode)
                {
                    var request2 = new RestRequest(endpointByRoles, Method.Get);
                    var response2 = await client.ExecuteAsync(request2);
                    var roleList = response2.Content;
                    var roleListJArray = Newtonsoft.Json.JsonConvert.DeserializeObject<JArray>(roleList!);
                    ViewBag.RoleListJArray = roleListJArray;
                    var jsonData = response.Content;
                    var student = JsonConvert.DeserializeObject<Student>(jsonData is not null ? jsonData : "");
                    return View(student);

                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home", ex.Message);
            }
            return View();
        }
    }
}
