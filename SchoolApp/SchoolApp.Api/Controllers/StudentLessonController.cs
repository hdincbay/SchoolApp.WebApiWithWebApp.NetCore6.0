using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SchoolApp.Api.ViewModels.StudentLessonViewModels;
using SchoolApp.Services.Contracts;
using System.Text.Json.Nodes;

namespace SchoolApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentLessonController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public StudentLessonController(IServiceManager manager)
        {
            _manager = manager;
        }
        [HttpGet("GetAllStudentWithLesson")]
        public async Task<IActionResult> GetAllStudentWithLesson(int id)
        {
            try
            {
                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
