using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.DTO.Marks;

public class CreateMarksDTO
{
    [Required]
    public Guid StudentSubjectId { get; set; }
    [Range(0, 100, ErrorMessage = "Total mark must be between 0 and 100.")]
    public double TotalMark { get; set; }
}
