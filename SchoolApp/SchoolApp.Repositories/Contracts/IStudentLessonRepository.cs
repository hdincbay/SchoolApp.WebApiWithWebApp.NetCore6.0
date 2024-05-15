using SchoolApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Repositories.Contracts
{
    public interface IStudentLessonRepository : IRepositoryBase<StudentLesson>
    {
        public Task<IQueryable<StudentLesson>> GetAllStudentLessons(bool trackChanges);
        public Task<StudentLesson?> GetOneStudentLesson(int id, bool trackChanges);
        public Task CreateOneStudentLesson(StudentLesson studentLesson);
        public Task UpdateOneStudentLesson(StudentLesson studentLesson);
        public Task DeleteOneStudentLesson(StudentLesson studentLesson);
    }
}
