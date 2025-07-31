using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models.Entities
{
    public class RegisterUser
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string ConfirmPassword { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Role { get; set; } = "Student";
        public string ID { get; set; } = string.Empty;

        //navigation properties
        public ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();

    }
}