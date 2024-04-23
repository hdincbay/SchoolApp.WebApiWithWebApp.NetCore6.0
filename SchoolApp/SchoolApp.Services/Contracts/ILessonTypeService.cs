using SchoolApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Services.Contracts
{
    public interface ILessonTypeService
    {
        public Task<IEnumerable<LessonType>> GetAll(bool trackChanges);
        public Task<LessonType?> GetOne(int id, bool trackChanges);
        public Task CreateOne(LessonType lessonType);
        public Task UpdateOne(LessonType lessonType);
        public Task DeleteOne(LessonType lessonType);
    }
}
