using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.Entities
{
    public class StudentSubject
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }

        //navigation properties
        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; }
    }
}
