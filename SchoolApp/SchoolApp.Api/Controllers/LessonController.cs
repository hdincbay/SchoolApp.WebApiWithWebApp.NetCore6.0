using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Api.ViewModels.LessonViewModels;
using SchoolApp.Entities.Models;
using SchoolApp.Services.Contracts;

namespace SchoolApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public LessonController(IServiceManager manager)
        {
            _manager = manager;
        }
        [HttpGet("GetAllLessons")]
        public async Task<IActionResult> GetAllLessons()
        {
            try
            {
                var lessonList = await _manager.LessonService.GetAll(false);
                return Ok(lessonList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("GetOneLesson/{id:int}")]
        public async Task<IActionResult> GetOneLesson([FromRoute] int id)
        {
            try
            {
                var lesson = await _manager.LessonService.GetOne(id, false);
                return Ok(lesson);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost("CreateOneLesson")]
        public async Task<IActionResult> CreateOneLesson([FromBody] CreateLessonViewModel createLessonViewModel)
        {
            try
            {
                var lesson = new Lesson()
                {
                    LessonName = createLessonViewModel.LessonName,
                    Description = createLessonViewModel.Description,
                    LessonTypeId = createLessonViewModel.LessonTypeId,
                    Price = createLessonViewModel.Price,
                };
                await _manager.LessonService.CreateOne(lesson);
                return Ok("Ders eklendi.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpPost("UpdateLesson/{id:int}")]
        public async Task<IActionResult> UpdateLesson([FromBody] UpdateLessonViewModel updateLessonViewModel, [FromRoute] int id)
        {
            try
            {
                var lesson = await _manager.LessonService.GetOne(id, true);
                if (lesson is not null)
                {
                    lesson.Description = updateLessonViewModel.Description;
                    lesson.LessonName = updateLessonViewModel.LessonName;
                    lesson.LessonTypeId = updateLessonViewModel.LessonTypeId;
                    lesson.Price = updateLessonViewModel.Price;
                    await _manager.LessonService.UpdateOne(lesson);
                    return Ok("Ders güncellendi.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteLesson([FromRoute] int id)
        {
            try
            {
                var lesson = await _manager.LessonService.GetOne(id, true);
                if (lesson is not null)
                {
                    await _manager.LessonService.DeleteOne(lesson);
                    return Ok("Ders silindi");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
            return NoContent();
        }
    }
}
