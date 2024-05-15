using SchoolApp.Entities.Models;
using SchoolApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Repositories.Concrete
{
    public class StudentLessonRepository : RepositorBase<StudentLesson>, IStudentLessonRepository
    {
        public StudentLessonRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task CreateOneStudentLesson(StudentLesson studentLesson) => await Create(studentLesson);

        public async Task DeleteOneStudentLesson(StudentLesson studentLesson) => await Delete(studentLesson);

        public async Task<IQueryable<StudentLesson>> GetAllStudentLessons(bool trackChanges) => await  FindAll(trackChanges);

        public  async Task<StudentLesson?> GetOneStudentLessonByStudentId(int id, bool trackChanges) => await FindByCondition(sl => sl.Id.Equals(id), trackChanges);

        public Task UpdateOneStudentLesson(StudentLesson studentLesson) => Update(studentLesson);
    }
}
