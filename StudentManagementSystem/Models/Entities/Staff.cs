using System;
using System.ComponentModel.DataAnnotations;

namespace YourNamespace.Models
{
    public class Staff
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string StaffId { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; }= string.Empty;
        public string LastName { get; set; }=string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; }=string.Empty ;
        [Required]
        public string Phone { get; set; }=string.Empty;
        [Required]
        public string Department { get; set; }=string.Empty;
        [Required]
        public string Position { get; set; }=string.Empty;
        public DateTime HireDate { get; set; }=DateTime.Now;
        [Required]
        public string Role { get; set; }=string.Empty;
        [Required]
        public string Status { get; set; }=String.Empty;
    }
}
