using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models.Entities
{
    public class Mark
    {
        public Guid Id { get; set; }
        public Guid StudentSubjectId { get; set; }
      
        public double TotalMark { get; set; }

        // Navigation properties
        [ForeignKey(nameof(StudentSubjectId))]
        public StudentSubject StudentSubject { get; set; } = new StudentSubject();
    }
}
