using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; }= string.Empty;
        public string LastName { get; set; }=string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Password{ get; set; } = string.Empty;
        public string ConfirmPassword { get; set; }= string.Empty;
        public string Role { get; set; } = "Student";
        public string StudentOrStaffNo { get; set; } = string.Empty;
        
        //navigation properties
        public ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();

    }
}
