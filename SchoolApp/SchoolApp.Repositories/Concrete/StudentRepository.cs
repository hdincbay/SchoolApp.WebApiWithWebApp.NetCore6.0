using SchoolApp.Entities.Models;
using SchoolApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Repositories.Concrete
{
    public class StudentRepository : RepositorBase<Student>, IStudentRepository
    {
        public StudentRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task CreateOneStudent(Student student) => await Create(student);

        public async Task DeleteOneStudent(Student student) => await Delete(student);

        public async Task<IQueryable<Student>> GetAllStudents(bool trackChanges) => await FindAll(trackChanges);

        public async Task<Student?> GetOneStudent(int id, bool trackChanges) => await FindByCondition(s => s.StudentId.Equals(id), trackChanges);

        public async Task UpdateOneStudent(Student student) => await Update(student);
    }
}
