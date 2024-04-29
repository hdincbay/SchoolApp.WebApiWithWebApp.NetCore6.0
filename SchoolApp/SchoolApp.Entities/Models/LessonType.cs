using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Entities.Models
{
    public class LessonType
    {
        public int LessonTypeId { get; set; }
        public string? LessonTypeName { get; set; }
        public ICollection<Lesson>? Lessons { get; set; }

    }
}
