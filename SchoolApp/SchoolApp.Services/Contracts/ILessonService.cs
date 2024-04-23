using SchoolApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Services.Contracts
{
    public interface ILessonService
    {
        public Task<IEnumerable<Lesson>> GetAll(bool trackChanges);
        public Task<Lesson?> GetOne(int id, bool trackChanges);
        public Task CreateOne(Lesson lesson);
        public Task UpdateOne(Lesson lesson);
        public Task DeleteOne(Lesson lesson);
    }
}
