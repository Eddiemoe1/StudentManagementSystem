namespace StudentManagementSystem.DTO.Subject
{
    public class GetSubjectDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}
