using SchoolApp.WebUI.Areas.Admin.Models;

namespace SchoolApp.WebUI.Models
{
    public class Lesson
    {
        public int LessonId { get; set; }
        public string? LessonName { get; set; }
        public int LessonTypeId { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public ICollection<Student>? Students { get; set; }
    }
}
