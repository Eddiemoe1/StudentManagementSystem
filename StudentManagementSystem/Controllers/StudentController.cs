using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.DTO.Student;
using StudentManagementSystem.Models.Entities;

namespace StudentManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _context.Students
                .Select(s => new
                {
                    s.Id,
                    s.StudentNo,
                    s.FirstName,
                    s.LastName,
                    s.Email,
                    s.PhoneNumber,
                    s.DateOfBirth,
                    s.Address,
                    s.EnrollmentDate,
                    Status = "active" 
                })
                .ToListAsync();

            return Ok(students);
        }
        // POST: api/Students
        [HttpPost]
        public async Task<IActionResult> AddStudent(StudentDTO dto)
        {
            var student = new Student
            {
                Id = Guid.NewGuid(),
                StudentNo = dto.StudentNo,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                EnrollmentDate = DateTime.UtcNow

            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return Ok(student);
        }
    }
}
