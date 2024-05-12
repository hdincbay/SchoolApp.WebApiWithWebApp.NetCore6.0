namespace SchoolApp.Api.ViewModels.StudentLessonViewModels
{
    public class GetStudentLessonViewModel
    {
        public int StudentId { get; set; }
        public int LessonId { get; set; }
        public string? StudentName { get; set; }
        public string? LessonName { get; set; }
    }
}
