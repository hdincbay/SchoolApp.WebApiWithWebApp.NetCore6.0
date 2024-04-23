using SchoolApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Repositories.Contracts
{
    public interface IStudentRepository : IRepositoryBase<Student>
    {
        public Task<IQueryable<Student>> GetAllStudents(bool trackChanges);
        public Task<Student?> GetOneStudent(int id, bool trackChanges);
        public Task CreateOneStudent(Student student);
        public Task UpdateOneStudent(Student student);
        public Task DeleteOneStudent(Student student);

    }
}
