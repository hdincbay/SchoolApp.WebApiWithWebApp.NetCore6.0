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
        public async Task<IActionResult> GetAllStudentWithLesson()
        {
            try
            {
                var students = await _manager.StudentService.GetAll(false);
                students = students?.AsQueryable()?.Include(sl => sl.StudentLessons!).ThenInclude(l => l.Lesson).ToList();

                var lessons = await _manager.LessonService.GetAll(false);
                lessons = lessons?.AsQueryable()?.Include(sl => sl.StudentLessons!).ThenInclude(s => s.Student).ToList();

                var getStudentLessonViewModel = new GetStudentLessonViewModel()
                {
                    LessonId = lessons!.Select(l => l.LessonId).SingleOrDefault(),
                    StudentId = students!.Select(s => s.StudentId).SingleOrDefault(),
                    LessonName = lessons!.Select(l => l.LessonName).SingleOrDefault(),
                    StudentName = students!.Select(s => s.StudentName).SingleOrDefault()
                };
                return Ok(getStudentLessonViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
