namespace StudentManagementSystem.DTO.User
{
    public class AddUpdateUserDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "Student";
        public string StudentOrStaffNo { get; set; } = string.Empty;

    }
}
