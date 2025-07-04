using StudentManagementSystem.DTO.Subject;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.DTO.Student
{
    public class GetStudentDTO
    {
        public Guid Id { get; set; }= Guid.NewGuid();
        public string FirstName { get; set; }=string.Empty;
        public string LastName { get; set; }=string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }=string.Empty;
        public string PhoneNumber { get; set; }=string.Empty;
        public string Address { get; set; } =string.Empty;
        // List of subjects
        public List<GetSubjectDTO> Subjects { get; set; } = new List<GetSubjectDTO>();
    }
}
