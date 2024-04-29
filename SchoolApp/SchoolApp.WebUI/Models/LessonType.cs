namespace SchoolApp.WebUI.Models
{
    public class LessonType
    {
        public int LessonTypeId { get; set; }
        public string? LessonTypeName { get; set; }
        public ICollection<Lesson>? Lessons { get; set; }
    }
}
