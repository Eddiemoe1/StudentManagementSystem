using System.Text.Json.Serialization;

namespace StudentManagementSystem.DTO.Auth
{
    public class RegisterUserDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        [JsonPropertyName("confirmPassword")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string Role { get; set; } = "Student";

        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
    }
}
