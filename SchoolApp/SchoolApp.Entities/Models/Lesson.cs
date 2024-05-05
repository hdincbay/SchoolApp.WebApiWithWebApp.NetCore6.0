using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Entities.Models
{
    public class Lesson
    {
        public int LessonId { get; set; }
        public string? LessonName { get; set; }
        public int LessonTypeId { get; set; }
        public LessonType? LessonType { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int Capacity { get; set; } = 10;
        public ICollection<Student>? Students { get; set; }
    }
}
