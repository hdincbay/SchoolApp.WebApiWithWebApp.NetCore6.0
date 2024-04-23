using SchoolApp.Entities.Models;
using SchoolApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Repositories.Concrete
{
    public class LessonRepository : RepositorBase<Lesson>, ILessonRepository
    {
        public LessonRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task CreateOneLesson(Lesson lesson) => await Create(lesson);

        public async Task DeleteOneLesson(Lesson lesson) => await Delete(lesson);

        public async Task<IQueryable<Lesson>> GetAllLessons(bool trackChanges) => await FindAll(trackChanges);

        public async Task<Lesson?> GetOneLesson(int id, bool trackChanges) => await FindByCondition(l => l.LessonId.Equals(id), trackChanges);

        public async Task UpdateOneLesson(Lesson lesson) => await Update(lesson);
    }
}
