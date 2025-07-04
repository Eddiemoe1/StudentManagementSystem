namespace StudentManagementSystem.DTO.User
{
    public class UserResponse
    {
        public record SubjectResponse(bool Flag, string Message = null!);
    }

}
