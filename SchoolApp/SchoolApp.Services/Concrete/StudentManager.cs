using SchoolApp.Entities.Models;
using SchoolApp.Repositories.Contracts;
using SchoolApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Services.Concrete
{
    public class StudentManager : IStudentService
    {
        private readonly IRepositoryManager _manager;

        public StudentManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public async Task CreateOne(Student student)
        {
            await _manager.StudentRepository.CreateOneStudent(student);
            _manager.Save();
        }

        public async Task DeleteOne(Student student)
        {
            var model = await _manager.StudentRepository.GetOneStudent(student.StudentId, true);
            if(model is not null)
            {
                await _manager.StudentRepository.DeleteOneStudent(model);
                _manager.Save();
            }
        }

        public async Task<IEnumerable<Student>> GetAll(bool trackChanges)
        {
            return await _manager.StudentRepository.GetAllStudents(trackChanges);
        }

        public async Task<Student?> GetOne(int id, bool trackChanges)
        {
            return await _manager.StudentRepository.GetOneStudent(id, trackChanges);
        }

        public async Task UpdateOne(Student student)
        {
            var model = await _manager.StudentRepository.GetOneStudent(student.StudentId, true);
            if(model is not null)
            {
                await _manager.StudentRepository.UpdateOneStudent(model);
                _manager.Save();
            }
        }
    }
}
