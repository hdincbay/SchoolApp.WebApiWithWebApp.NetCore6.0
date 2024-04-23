using SchoolApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Services.Concrete
{
    public class ServiceManager : IServiceManager
    {
        private readonly ILessonService _lessonService;
        private readonly ILessonTypeService _lessonTypeService;
        private readonly IStudentService _studentService;

        public ServiceManager(ILessonService lessonService, ILessonTypeService lessonTypeService, IStudentService studentService)
        {
            _lessonService = lessonService;
            _lessonTypeService = lessonTypeService;
            _studentService = studentService;
        }

        public ILessonService LessonService => _lessonService;

        public ILessonTypeService LessonTypeService => _lessonTypeService;

        public IStudentService StudentService => _studentService;
    }
}
