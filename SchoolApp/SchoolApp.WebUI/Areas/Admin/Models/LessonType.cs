namespace SchoolApp.WebUI.Areas.Admin.Models
{
    public class LessonType
    {
        public int LessonTypeId { get; set; }
        public string? LessonTypeName { get; set; }
        public ICollection<Lesson>? Lessons { get; set; }
    }
}
