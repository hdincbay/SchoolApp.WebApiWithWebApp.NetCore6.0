using log4net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using SchoolApp.WebUI.Models;

namespace SchoolApp.WebUI.Controllers
{
    public class StudentController : Controller
    {
        private readonly ILog log = LogManager.GetLogger(typeof(StudentController));

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var resource = "https://localhost:7081/api/Student/GetAllStudents";
                log.Debug("Öğrenci Listesinin endpointi: " + resource);
                var client = new RestClient(resource);
                log.Debug("Client oluşturuldu.");
                var request = new RestRequest(resource, Method.Get);
                log.Debug("Get methodu ile Request oluşturuldu.");
                var response = await client.ExecuteAsync(request);
                log.Debug("İstek gerçekleştiriliyor...");
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
            catch(Exception ex) 
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
        public async Task<IActionResult> Create([FromForm]Student student, IFormFile file)
        {
            try
            {
                var resource = "https://localhost:7081/api/Student/CreateStudent";
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
                    TempData["ServiceResponse"] = "Öğrenci başarıyla oluşturuldu";
                }
                else
                {
                    log.Error("Öğrenci oluşturma işlemi başarısız oldu.");
                }
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", ex.Message);
            }
        }
    }
}
