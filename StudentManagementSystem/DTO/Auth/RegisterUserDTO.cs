using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace StudentManagementSystem.DTO.Auth
{
    public class RegisterUserDTO
    {
        //public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
