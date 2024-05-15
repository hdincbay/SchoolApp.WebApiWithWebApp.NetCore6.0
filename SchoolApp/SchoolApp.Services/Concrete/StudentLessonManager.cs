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
    public class StudentLessonManager : IStudentLessonService
    {
        private readonly IRepositoryManager _manager;

        public StudentLessonManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public async Task CreateOne(StudentLesson studentLesson)
        {
            await _manager.StudentLessonRepository.CreateOneStudentLesson(studentLesson);
            _manager.Save();
        }

        public async Task DeleteOne(StudentLesson studentLesson)
        {
            var model = await _manager.StudentLessonRepository.GetOneStudentLesson(studentLesson.Id, true);
            if(model is not null)
            {
                await _manager.StudentLessonRepository.DeleteOneStudentLesson(model);
                _manager.Save();
            }
        }

        public async Task<IEnumerable<StudentLesson>> GetAll(bool trackChanges)
        {
            return await _manager.StudentLessonRepository.GetAllStudentLessons(trackChanges);
        }

        public async Task<StudentLesson?> GetOne(int id, bool trackChanges)
        {
            return await _manager.StudentLessonRepository.GetOneStudentLesson(id, trackChanges);
        }

        public async Task UpdateOne(StudentLesson studentLesson)
        {
            var model = await _manager.StudentLessonRepository.GetOneStudentLesson(studentLesson.Id, true);
            if(model is not null)
            {
                await _manager.StudentLessonRepository.UpdateOneStudentLesson(model);
                _manager.Save();
            }
        }
    }
}
