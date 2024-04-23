using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Repositories.Contracts
{
    public interface IRepositoryManager
    {
        public ILessonRepository LessonRepository { get; }
        public ILessonTypeRepository LessonTypeRepository { get; }
        public IStudentRepository StudentRepository { get; }
        public void Save();
    }
}
