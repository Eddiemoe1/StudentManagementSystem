using System;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.DTO.Staff
{
    public class UpdateStaffDto
    {
        [Required] public string Id { get; set; } = string.Empty;
        [Required] public string StaffId { get; set; } = string.Empty;
        [Required] public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [Required, EmailAddress] public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public DateTime? HireDate { get; set; }
        public string Role { get; set; } = "lecturer";
        public string Status { get; set; } = "active";
    }
}
