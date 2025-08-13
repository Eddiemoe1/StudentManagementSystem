using StudentManagementSystem.DTO.Subject;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.DTO.Student
{
    public class StudentDTO
    {
        public string StudentNo { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime? EnrollmentDate { get; set; }
}
}
