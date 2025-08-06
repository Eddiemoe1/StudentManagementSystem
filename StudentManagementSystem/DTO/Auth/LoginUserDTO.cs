namespace StudentManagementSystem.DTO.Auth
{
    public class LoginUserDTO
    {
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
