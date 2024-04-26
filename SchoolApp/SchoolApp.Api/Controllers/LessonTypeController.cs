using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Api.ViewModels.LessonTypeViewModels;
using SchoolApp.Entities.Models;
using SchoolApp.Services.Contracts;

namespace SchoolApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonTypeController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public LessonTypeController(IServiceManager manager)
        {
            _manager = manager;
        }
        [HttpGet("GetAllLessonType")]
        public async Task<IActionResult> GetAllLessonTypes()
        {
            try
            {
                var lessonTypes = await _manager.LessonTypeService.GetAll(false);
                return Ok(lessonTypes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetOneLessonType/{id:int}")]
        public async Task<IActionResult> GetOneLessonType([FromRoute] int id)
        {
            try
            {
                var lessonType = await _manager.LessonTypeService.GetOne(id, false);
                return Ok(lessonType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("CreateLessonType")]
        public async Task<IActionResult> CreateLessonType([FromBody] CreateLessonTypeViewModel createlessonTypeViewModel)
        {
            try
            {
                var lessonType = new LessonType()
                {
                    LessonTypeName = createlessonTypeViewModel.LessonTypeName
                };
                await _manager.LessonTypeService.CreateOne(lessonType);
                return Ok("Ders Tipi Eklendi");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("UpdateLessonType/{id:int}")]
        public async Task<IActionResult> UpdateLessonType([FromBody] UpdateLessonTypeViewModel updateLessonTypeViewModel, [FromRoute] int id)
        {
            try
            {
                var lessonType = await _manager.LessonTypeService.GetOne(id, true);
                if (lessonType is not null)
                {
                    lessonType.LessonTypeName = updateLessonTypeViewModel.LessonTypeName;
                    await _manager.LessonTypeService.UpdateOne(lessonType);
                    return Ok("Ders tipi güncellendi.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteLessonType([FromRoute] int id)
        {
            try
            {
                var lessonType = await _manager.LessonTypeService.GetOne(id, true);
                if (lessonType is not null)
                {
                    await _manager.LessonTypeService.DeleteOne(lessonType);
                    return Ok("Ders tipi silindi.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }
    }
}
