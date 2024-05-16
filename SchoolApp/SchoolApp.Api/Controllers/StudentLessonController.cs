using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SchoolApp.Api.ViewModels.StudentLessonViewModels;
using SchoolApp.Entities.Models;
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
                var studenWithLessonList = await _manager.StudentLessonService.GetAll(false);

                return Ok(studenWithLessonList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetOneStudentWithLesson/{id:int}")]
        public async Task<IActionResult> GetAllLessonByStudentId([FromRoute]int id)
        {
            try
            {
                var student = await _manager.StudentService.GetOne(id, true);
                if(student is not null)
                {
                    var studentWithLessonList = await _manager.StudentLessonService.GetAll(false);
                    var lessonListByStudentId = studentWithLessonList.AsQueryable().Where(l => l.StudentId.Equals(id)).Select(l => l.LessonId).ToList();
                    return Ok(lessonListByStudentId);
                }
                else
                {
                    return BadRequest("Öğrenci bulunamadı!");
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAllStudentByLessonId/{id:int}")]
        public async Task<IActionResult> GetAllStudentByLessonId([FromRoute]int id)
        {
            try
            {
                var lesson = await _manager.LessonService.GetOne(id, true);
                if (lesson is not null)
                {
                    var studentWithLessonList = await _manager.StudentLessonService.GetAll(false);
                    var studentListByLessonId = studentWithLessonList.AsQueryable().Where(s => s.StudentId.Equals(id)).Select(s => s.StudentId).ToList();
                    return Ok(studentListByLessonId);
                }
                else
                {
                    return BadRequest("Ders bulunamadı!");
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("CreateStudentLesson")]
        public async Task<IActionResult> CreateStudentLesson([FromBody] CreateStudentLessonViewModel createStudentLessonViewModel)
        {
            try
            {
                await _manager.StudentLessonService.CreateOne(new StudentLesson()
                {
                    LessonId = createStudentLessonViewModel.LessonId,
                    StudentId = createStudentLessonViewModel.StudentId
                });
                return Ok("Öğrenci-Ders ilişkisi eklendi.");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("UpdateStudentLesson/{id:int}")]
        public async Task<IActionResult> UpdateStudentLesson([FromBody] UpdateStudentLessonViewModel updateStudentLessonViewModel, [FromRoute] int id)
        {
            try
            {
                var studentLesson = await _manager.StudentLessonService.GetOne(id, true);
                if(studentLesson is not null)
                {
                    studentLesson.LessonId = updateStudentLessonViewModel.LessonId;
                    studentLesson.StudentId = updateStudentLessonViewModel.StudentId;
                    await _manager.StudentLessonService.UpdateOne(studentLesson);
                    return Ok("Öğrenci-Ders ilişkisi güncellendi.");
                }
                else
                {
                    return BadRequest("Öğrenci-Ders ilişkisi bulunamadı!");
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
