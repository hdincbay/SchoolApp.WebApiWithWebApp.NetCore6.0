using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Entities.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? StudentSurname { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<Lesson>? Lessons { get; set; }
        public int Credit { get; set; }

    }
}
