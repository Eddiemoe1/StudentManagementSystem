using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models.Entities;
using StudentManagementSystem.Models;
// using StudentManagementSystem.Models;

namespace StudentManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<Staff> Staffs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Make StudentSubjectId unique in Marks table
            modelBuilder.Entity<Mark>()
                .HasIndex(m => m.StudentSubjectId)
                .IsUnique();

            //StudentSubject relations
            modelBuilder.Entity<StudentSubject>()
                .HasKey(ss => ss.Id);

            modelBuilder.Entity<StudentSubject>()
                .HasOne(ss => ss.Student)
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.StudentId);

            modelBuilder.Entity<StudentSubject>()
                .HasOne(ss => ss.Subject)
                .WithMany(su => su.StudentSubjects)
                .HasForeignKey(ss => ss.SubjectId);
        }
    }
}


