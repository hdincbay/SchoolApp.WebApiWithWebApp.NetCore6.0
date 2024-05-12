using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Repositories
{
    public class RepositoryContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Lesson>? Lessons { get; set; }
        public DbSet<LessonType>? LessonTypes { get; set; }
        public DbSet<Student>? Students { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
