namespace SchoolApp.Api.ViewModels.LessonViewModels
{
    public class CreateLessonViewModel
    {
        public string? LessonName { get; set; }
        public int LessonTypeId { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int Capacity { get; set; }
    }
}
