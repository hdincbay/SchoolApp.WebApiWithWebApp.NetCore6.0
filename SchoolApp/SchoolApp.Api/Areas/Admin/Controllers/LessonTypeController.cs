using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Api.Areas.ViewModels;
using SchoolApp.Entities.Models;
using SchoolApp.Services.Contracts;

namespace SchoolApp.Api.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class LessonTypeController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public LessonTypeController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLessonType([FromBody]LessonTypeViewModel lessonTypeViewModel)
        {
            var lessonType = new LessonType()
            {
                LessonTypeName = lessonTypeViewModel.LessonTypeName
            };
            await _manager.LessonTypeService.CreateOne(lessonType);
            return Ok("Ders Tipi Eklendi"); 
        }
    }
}
