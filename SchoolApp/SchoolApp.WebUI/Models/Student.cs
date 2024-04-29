namespace SchoolApp.WebUI.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? StudentSurname { get; set; }
        public ICollection<Lesson>? Lessons { get; set; }
        public string? ImageUrl { get; set; }
        public int Credit { get; set; }
    }
}
