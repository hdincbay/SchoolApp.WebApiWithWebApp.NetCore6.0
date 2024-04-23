using SchoolApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Services.Contracts
{
    public interface IStudentService
    {
        public Task<IEnumerable<Student>> GetAll(bool trackChanges);
        public Task<Student?> GetOne(int id, bool trackChanges);
        public Task CreateOne(Student student);
        public Task UpdateOne(Student student);
        public Task DeleteOne(Student student);
    }
}
