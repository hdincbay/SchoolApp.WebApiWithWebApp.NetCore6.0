using SchoolApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Repositories.Contracts
{
    public interface ILessonTypeRepository : IRepositoryBase<LessonType>
    {
        public Task<IQueryable<LessonType>> GetAllLessonTypes(bool trackChanges);
        public Task<LessonType?> GetOneLessonType(int id, bool trackChanges);
        public Task CreateOneLessonType(LessonType lessonType);
        public Task UpdateOneLessonType(LessonType lessonType);
        public Task DeleteOneLessonType(LessonType lessonType);
    }
}
