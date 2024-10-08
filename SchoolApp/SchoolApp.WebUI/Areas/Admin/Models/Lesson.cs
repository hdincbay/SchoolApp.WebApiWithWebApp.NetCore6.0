﻿namespace SchoolApp.WebUI.Areas.Admin.Models
{
    public class Lesson
    {
        public int LessonId { get; set; }
        public string? LessonName { get; set; }
        public int LessonTypeId { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public int Capacity { get; set; } = 10;
        public ICollection<Student>? Students { get; set; }
    }
}
