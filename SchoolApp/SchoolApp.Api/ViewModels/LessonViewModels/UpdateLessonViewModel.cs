namespace SchoolApp.Api.ViewModels.LessonViewModels
{
    public class UpdateLessonViewModel
    {
        public string? LessonName { get; set; }
        public int LessonTypeId { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }
}
