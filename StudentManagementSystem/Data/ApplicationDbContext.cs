using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models.Entities;

namespace StudentManagementSystem.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet< Subject> Subjects { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional model configuration can go here
        }
    }    
}

