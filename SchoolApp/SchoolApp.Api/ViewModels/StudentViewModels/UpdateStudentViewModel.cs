using SchoolApp.Entities.Models;

namespace SchoolApp.Api.ViewModels.StudentViewModels
{
    public class UpdateStudentViewModel
    {
        public string? StudentName { get; set; }
        public string? StudentSurname { get; set; }
        public int Credit { get; set; }
    }
}
