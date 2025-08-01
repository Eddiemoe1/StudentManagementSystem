﻿using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace StudentManagementSystem.DTO.Auth
{
    public class RegisterUserDTO
    {
        //public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string confirmPassword { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Role { get; set; } = "Student";

        public string ID { get; set; } = string.Empty;
        
    }
}
