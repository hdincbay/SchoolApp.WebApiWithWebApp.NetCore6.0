using SchoolApp.Entities.Models;
using SchoolApp.Repositories.Contracts;
using SchoolApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Services.Concrete
{
    public class LessonTypeManager : ILessonTypeService
    {
        private readonly IRepositoryManager _manager;

        public LessonTypeManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public async Task CreateOne(LessonType lessonType)
        {
            await _manager.LessonTypeRepository.CreateOneLessonType(lessonType);
            _manager.Save();
        }

        public async Task DeleteOne(LessonType lessonType)
        {
            var model = await _manager.LessonTypeRepository.GetOneLessonType(lessonType.LessonTypeId, true);
            if(model is not null)
            {
                await _manager.LessonTypeRepository.DeleteOneLessonType(model);
                _manager.Save();
            }
        }

        public async Task<IEnumerable<LessonType>> GetAll(bool trackChanges)
        {
            return await _manager.LessonTypeRepository.GetAllLessonTypes(trackChanges);
        }

        public async Task<LessonType?> GetOne(int id, bool trackChanges)
        {
            return await _manager.LessonTypeRepository.GetOneLessonType(id, trackChanges);
        }

        public async Task UpdateOne(LessonType lessonType)
        {
            var model = await _manager.LessonTypeRepository.GetOneLessonType(lessonType.LessonTypeId, true);
            if(model is not null)
            {
                await _manager.LessonTypeRepository.UpdateOneLessonType(model);
                _manager.Save();
            }
        }
    }
}
