using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SchoolApp.Api.ViewModels.StudentLessonViewModels;
using SchoolApp.Entities.Models;
using SchoolApp.Repositories;
using SchoolApp.Services.Contracts;
using System.Text.Json.Nodes;

namespace SchoolApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentLessonController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly RepositoryContext _context;

        public StudentLessonController(IServiceManager manager, RepositoryContext context)
        {
            _manager = manager;
            _context = context;
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
                var slList = await _manager.StudentLessonService.GetAll(false);
                var slListQuery = slList.AsQueryable();
                var slListQuerySearch = slListQuery.Where(sl => sl.LessonId.Equals(createStudentLessonViewModel.LessonId) && sl.StudentId.Equals(createStudentLessonViewModel.StudentId));
                if (slListQuerySearch is not null)
                    return BadRequest("Öğrenci-Ders ilişkisi bulunmaktadır.");
                
                await _manager.StudentLessonService.CreateOne(new StudentLesson()
                {
                    LessonId = createStudentLessonViewModel.LessonId,
                    StudentId = createStudentLessonViewModel.StudentId
                });
                var lesson = await _manager.LessonService.GetOne(createStudentLessonViewModel.LessonId, true);
                if (lesson is not null)
                    lesson.Capacity = lesson.Capacity - 1;
                await _manager.LessonService.UpdateOne(lesson!);
                return Ok("Öğrenci-Ders ilişkisi eklendi.");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteStudentLesson")]
        public async Task<IActionResult> DeleteStudentLesson([FromBody] UpdateStudentLessonViewModel updateStudentLessonViewModel)
        {
            try
            {
                var studentLessonList = await _manager.StudentLessonService.GetAll(false);
                var studentLessonQuery = studentLessonList.AsQueryable();
                var studentLessonSearch = studentLessonQuery.Where(sl => sl.StudentId.Equals(updateStudentLessonViewModel.StudentId) && sl.LessonId.Equals(updateStudentLessonViewModel.LessonId)).SingleOrDefault();
                if (studentLessonSearch is null)
                    return BadRequest("Öğrenci-Ders ilişkisi bulunamadı.");
                var lesson = await _manager.LessonService.GetOne(updateStudentLessonViewModel.LessonId, true);
                if(lesson is not null)
                    lesson.Capacity = lesson.Capacity + 1;
                
                await _manager.LessonService.UpdateOne(lesson!);
                await _manager.StudentLessonService.DeleteOne(studentLessonSearch);
                return Ok("Öğrenci-Ders ilişkisi kaldırıldı.");
                
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
