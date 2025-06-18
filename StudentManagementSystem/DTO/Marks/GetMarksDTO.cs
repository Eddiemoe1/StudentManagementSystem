using StudentManagementSystem.Models.Entities;

namespace StudentManagementSystem.DTO.Marks
{
    public class GetMarksDTO
    {
        public Guid Id { get; set; }
        public Guid StudentSubjectId { get; set; }

        public double TotalMark { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
    }
}
