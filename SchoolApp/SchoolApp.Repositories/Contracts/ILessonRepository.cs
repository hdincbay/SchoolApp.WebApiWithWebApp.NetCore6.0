using SchoolApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Repositories.Contracts
{
    public interface ILessonRepository : IRepositoryBase<Lesson>
    {
        public Task<IQueryable<Lesson>> GetAllLessons(bool trackChanges);
        public Task<Lesson?> GetOneLesson(int id, bool trackChanges);
        public Task CreateOneLesson(Lesson lesson);
        public Task UpdateOneLesson(Lesson lesson);
        public Task DeleteOneLesson(Lesson lesson);
    }
}
