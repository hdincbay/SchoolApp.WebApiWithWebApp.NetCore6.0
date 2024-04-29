using SchoolApp.Entities.Models;

namespace SchoolApp.Api.ViewModels.StudentViewModels
{
    public class CreateStudentViewModel
    {
        public string? StudentName { get; set; }
        public string? StudentSurname { get; set; }
        public int Credit { get; set; }
        public string? ImageUrl { get; set; }

    }
}
