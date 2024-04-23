using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Services.Contracts
{
    public interface IServiceManager
    {
        public ILessonService LessonService { get; }
        public ILessonTypeService LessonTypeService { get; }
        public IStudentService StudentService { get; }
    }
}
