using StudentManagementSystem.Models.Entities;

namespace StudentManagementSystem.DTO.Marks
{
    public class GetMarksDTO
    {
        public Guid Id { get; set; }
        public Guid StudentSubjectId { get; set; }

        public double TotalMark { get; set; }
        public StudentSubject StudentSubject { get; internal set; }
        public object TotalMarks { get; internal set; }
    }
}
