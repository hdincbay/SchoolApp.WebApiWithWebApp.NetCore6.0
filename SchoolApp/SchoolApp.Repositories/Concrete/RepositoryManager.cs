using SchoolApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Repositories.Concrete
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ILessonTypeRepository _lessonTypeRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentLessonRepository _studentLessonRepository;
        private readonly RepositoryContext _context;

        public RepositoryManager(ILessonRepository lessonRepository, ILessonTypeRepository lessonTypeRepository, IStudentRepository studentRepository, RepositoryContext context, IStudentLessonRepository studentLessonRepository)
        {
            _lessonRepository = lessonRepository;
            _lessonTypeRepository = lessonTypeRepository;
            _studentRepository = studentRepository;
            _context = context;
            _studentLessonRepository = studentLessonRepository;
        }

        public ILessonRepository LessonRepository => _lessonRepository;

        public ILessonTypeRepository LessonTypeRepository => _lessonTypeRepository;

        public IStudentRepository StudentRepository => _studentRepository;

        public IStudentLessonRepository StudentLessonRepository => _studentLessonRepository;

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
