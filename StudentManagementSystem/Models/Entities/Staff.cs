using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models.Entities
{
    public class Staff
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(50)]
        public string StaffId { get; set; } = string.Empty; 

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Department { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Position { get; set; } = string.Empty;

        public DateTime? HireDate { get; set; }

        [MaxLength(50)]
        public string Role { get; set; } = "lecturer"; 

        [MaxLength(50)]
        public string Status { get; set; } = "active";

        
    }
}
