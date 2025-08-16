namespace StudentManagementSystem.Models.Entities
{
    public class Student
    {
        public Guid Id { get; set; }
        public String StudentNo { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public string status { get; set; } = string.Empty;

        public DateTime? EnrollmentDate { get; set; }
       


        // navigation properties
        public ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();
    }
}


