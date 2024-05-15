using SchoolApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Services.Contracts
{
    public interface IStudentLessonService
    {
        public Task<IEnumerable<StudentLesson>> GetAll(bool trackChanges);
        public Task<StudentLesson?> GetOne(int id, bool trackChanges);
        public Task CreateOne(StudentLesson studentLesson);
        public Task UpdateOne(StudentLesson studentLesson);
        public Task DeleteOne(StudentLesson studentLesson);
    }
}
