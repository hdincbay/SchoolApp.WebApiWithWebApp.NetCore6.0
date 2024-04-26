﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Api.ViewModels.StudentViewModels;
using SchoolApp.Entities.Models;
using SchoolApp.Services.Contracts;

namespace SchoolApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public StudentController(IServiceManager manager)
        {
            _manager = manager;
        }
        [HttpGet("GetAllStudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var studentList = await _manager.StudentService.GetAll(false);
                return Ok(studentList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetOneStudent/{id:int}")]
        public async Task<IActionResult> GetOneStudent([FromRoute] int id)
        {
            try
            {
                var student = await _manager.StudentService.GetOne(id, false);
                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("CreateStudent")]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentViewModel createStudentViewModel)
        {
            try
            {
                await _manager.StudentService.CreateOne(new Student()
                {
                    Credit = createStudentViewModel.Credit,
                    StudentName = createStudentViewModel.StudentName,
                    StudentSurname = createStudentViewModel.StudentSurname,
                });
                return Ok("Öğrenci eklendi.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("UpdateStudent/{id:int}")]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentViewModel updateStudentViewModel, [FromRoute] int id)
        {
            try
            {
                var student = await _manager.StudentService.GetOne(id, true);
                if (student is not null)
                {
                    student.StudentName = updateStudentViewModel.StudentName;
                    student.StudentSurname = updateStudentViewModel.StudentSurname;
                    student.Credit = updateStudentViewModel.Credit;
                    await _manager.StudentService.UpdateOne(student);
                    return Ok("Öğrenci güncellendi.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {
            try
            {
                var student = await _manager.StudentService.GetOne(id, true);
                if (student is not null)
                {
                    await _manager.StudentService.DeleteOne(student);
                }
                return Ok("Öğrenci silindi.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
