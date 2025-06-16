namespace StudentManagementSystem.DTO.Student
{
    public class AddUpdateStudent
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } 
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        // List of subjects
        public List<Guid> SubjectIds { get; set; } = new List<Guid>();
    }
}
