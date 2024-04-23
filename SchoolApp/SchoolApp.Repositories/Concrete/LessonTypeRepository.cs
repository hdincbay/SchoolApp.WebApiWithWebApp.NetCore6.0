using SchoolApp.Entities.Models;
using SchoolApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Repositories.Concrete
{
    public class LessonTypeRepository : RepositorBase<LessonType>, ILessonTypeRepository
    {
        public LessonTypeRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task CreateOneLessonType(LessonType lessonType) => await Create(lessonType);

        public async Task DeleteOneLessonType(LessonType lessonType) => await Delete(lessonType);

        public async Task<IQueryable<LessonType>> GetAllLessonTypes(bool trackChanges) => await FindAll(trackChanges);

        public async Task<LessonType?> GetOneLessonType(int id, bool trackChanges) => await FindByCondition(t => t.LessonTypeId.Equals(id), trackChanges);

        public async Task UpdateOneLessonType(LessonType lessonType) => await Update(lessonType);
    }
}
