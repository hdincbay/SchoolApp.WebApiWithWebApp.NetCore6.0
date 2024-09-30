using log4net;
using log4net.Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using SchoolApp.WebUI.Models;

namespace SchoolApp.WebUI.Controllers
{
    public class LessonController : Controller
    {
        private readonly ILog log = LogManager.GetLogger(typeof(LessonController));
        private readonly IConfiguration _configuration;

        public LessonController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var apiEndpoint = _configuration["apiEndpointAddress"]?.ToString();
                string resource = string.Format("{0}/api/Lesson/GetAllLessons", apiEndpoint);
                log.Debug("Gidilecek endpoint: " + resource);
                var client = new RestClient();
                log.Debug("Client oluşturuldu.");
                var request = new RestRequest(resource, Method.Get);
                log.Debug("Request oluşturuldu.");
                var response = await client.ExecuteAsync(request);
                log.Debug("İstek gerçekleştiriliyor...");
                if (response.IsSuccessStatusCode)
                {
                    log.Debug("Ders listesi getirme isteği başarılı.");
                    var jsonData = response.Content;
                    log.Debug("Gelen json tipindeki veri: " + jsonData);
                    var lessons = JsonConvert.DeserializeObject<List<Lesson>>(jsonData is not null ? jsonData : "");
                   
                    
                    return View(lessons);
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
    }
}
