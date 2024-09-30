using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RestSharp;
using SchoolApp.WebUI.Areas.Admin.Models;

namespace SchoolApp.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LessonController : Controller
    {
        private readonly ILog log = LogManager.GetLogger(typeof(LessonController));
        private readonly IConfiguration _configuration;

        public LessonController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var apiEndpoint = _configuration["apiEndpointAddress"]?.ToString();
                var resource = string.Format("{0}/api/Lesson/GetAllLessons", apiEndpoint);
                log.Debug("Ders Listesinin endpointi: " + resource);
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
                    var values = JsonConvert.DeserializeObject<List<Lesson>>(jsonData is not null ? jsonData : "");
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
                return RedirectToAction("Error", "Home", ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                var apiEndpoint = _configuration["apiEndpointAddress"]?.ToString();
                var resource = string.Format("{0}/api/LessonType/GetAllLessonType", apiEndpoint);
                var client = new RestClient();
                var request = new RestRequest(resource, Method.Get);
                var response = await client.ExecuteAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = response.Content;
                    var values = JsonConvert.DeserializeObject<List<LessonType>>(jsonData is not null ? jsonData : "");
                    ViewBag.LessonTypeList = new SelectList(values, "LessonTypeId", "LessonTypeName", 1);
                }
                return View();
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error", "Home", ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]Lesson lesson)
        {
            try
            {
                var apiEndpoint = _configuration["apiEndpointAddress"]?.ToString();
                var resource = string.Format("{0}/api/Lesson/CreateOneLesson", apiEndpoint);
                log.Debug("Gidilecek endpoint: " + resource);
                var client = new RestClient();
                log.Debug("Client oluşturuldu.");
                var request = new RestRequest(resource, Method.Post);
                log.Debug("Post methodu ile Request oluşturuldu.");
                var jsonBody = JsonConvert.SerializeObject(lesson);
                log.Debug("Requestin Json Body'si: " + jsonBody);
                request.AddJsonBody(jsonBody);
                var response = await client.ExecuteAsync(request);
                if(response.IsSuccessStatusCode)
                {
                    ViewBag.ServiceResponse = "Ders oluşturma işlemi başarılı.";
                    log.Debug("Ders oluşturma işlemi başarılı.");
                }
                else
                {
                    ViewBag.ServiceResponse = "Ders oluşturma işlemi başarısız!";
                    log.Error("Ders oluşturma işlemi başarısız!");
                }
                return RedirectToAction("Index", "Lesson");

            }
            catch(Exception ex)
            {
                return RedirectToAction("Error", "Home", ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Update([FromRoute]int id)
        {
            try
            {
                var apiEndpoint = _configuration["apiEndpointAddress"]?.ToString();
                var resource = string.Format("{0}/api/LessonType/GetAllLessonType", apiEndpoint);
                var client = new RestClient();
                var request = new RestRequest(resource, Method.Get);
                var response = await client.ExecuteAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = response.Content;
                    var values = JsonConvert.DeserializeObject<List<LessonType>>(jsonData is not null ? jsonData : "");
                    ViewBag.LessonTypeList = new SelectList(values, "LessonTypeId", "LessonTypeName", 1);
                }
                var resource2 = string.Format("{0}/api/Lesson/GetOneLesson/{1}", apiEndpoint, id);
                var client2 = new RestClient();
                var request2 = new RestRequest(resource2, Method.Get);
                var response2 = await client.ExecuteAsync(request2);
                if(response2.IsSuccessStatusCode)
                {
                    var jsonData2 = response2.Content;
                    var lesson = JsonConvert.DeserializeObject<Lesson>(jsonData2 is not null ? jsonData2 : "");
                    if(lesson is not null)
                    {
                        return View(lesson);
                    }
                }
                
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error", "Home", ex.Message);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update([FromForm]Lesson lesson)
        {
            try
            {
                var apiEndpoint = _configuration["apiEndpointAddress"]?.ToString();
                var resource = string.Format("{0}/api/Lesson/UpdateLesson/{1}", apiEndpoint, lesson.LessonId);
                log.Debug("Gidilecek endpoint: " + resource);
                var client = new RestClient();
                log.Debug("Client oluşturuldu.");
                var request = new RestRequest(resource, Method.Post);
                log.Debug("Post methodu ile Request oluşturuldu.");
                var jsonBody = JsonConvert.SerializeObject(lesson);
                log.Debug("Requestin Json Body'si: " + jsonBody);
                request.AddJsonBody(jsonBody);
                var response = await client.ExecuteAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    log.Debug("Ders güncelleme işlemi başarılı.");
                }
                else
                {
                    log.Error("Ders güncelleme işlemi başarısız!");
                }
                return RedirectToAction("Index", "Lesson");

            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            try
            {
                var apiEndpoint = _configuration["apiEndpointAddress"]?.ToString();
                var resource = string.Format("{0}/api/Lesson/GetOneLesson/{}", apiEndpoint, id);
                var client = new RestClient();
                var request = new RestRequest(resource, Method.Get);
                var response = await client.ExecuteAsync(request);
                if(response.IsSuccessStatusCode)
                {
                    var jsonData = response.Content;
                    var lesson = JsonConvert.DeserializeObject<Lesson>(jsonData is not null ? jsonData : "");
                    if(lesson is not null)
                    {
                        return View(lesson);
                    }
                }
                return View();
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error", "Home", ex.Message);
            }
        }
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            try
            {
                var apiEndpoint = _configuration["apiEndpointAddress"]?.ToString();
                var resource = string.Format("{0}/api/Lesson/GetOneLesson/{1}", apiEndpoint, id);
                var client = new RestClient();
                var request = new RestRequest(resource, Method.Get);
                var response = await client.ExecuteAsync(request);
                if(response.IsSuccessStatusCode)
                {
                    var jsonData = response.Content;
                    var lesson = JsonConvert.DeserializeObject<Lesson>(jsonData is not null ? jsonData : "");
                    if(lesson is not null)
                    {
                        var resource2 = string.Format("{0}/api/Lesson/DeleteLesson/{1}", apiEndpoint, lesson.LessonId);
                        var client2 = new RestClient();
                        var request2 = new RestRequest(resource2, Method.Delete);
                        var response2 = await client2.ExecuteAsync(request2);
                        if(response2.IsSuccessStatusCode)
                        {
                            log.Debug("İstek başarılı.");
                        }
                    }
                    else
                    {
                        log.Error("Öğrenci bilgileri getirilemedi!");
                    }
                }
                return RedirectToAction("Index", "Lesson");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error", "Home", ex.Message);
            }
        }
    }
}
