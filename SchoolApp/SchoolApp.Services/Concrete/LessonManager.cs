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
    public class LessonManager : ILessonService
    {
        private readonly IRepositoryManager _manager;

        public LessonManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public async Task CreateOne(Lesson lesson)
        {
            await _manager.LessonRepository.CreateOneLesson(lesson);
            _manager.Save();
        }

        public async Task DeleteOne(Lesson lesson)
        {
            var model = await _manager.LessonRepository.GetOneLesson(lesson.LessonId, true);
            if(model is not null)
            {
                await _manager.LessonRepository.DeleteOneLesson(model);
                _manager.Save();
            }
        }

        public async Task<IEnumerable<Lesson>> GetAll(bool trackChanges)
        {
            return await _manager.LessonRepository.GetAllLessons(trackChanges);
        }

        public async Task<Lesson?> GetOne(int id, bool trackChanges)
        {
            return await _manager.LessonRepository.GetOneLesson(id, trackChanges);
        }

        public async Task UpdateOne(Lesson lesson)
        {
            var model = await _manager.LessonRepository.GetOneLesson(lesson.LessonId, true);
            if(model is not null)
            {
                await _manager.LessonRepository.UpdateOneLesson(model);
                _manager.Save();
            }
        }
    }
}
