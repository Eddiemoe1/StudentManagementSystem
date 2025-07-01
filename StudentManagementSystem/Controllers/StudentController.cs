using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.DTO.Student;
using StudentManagementSystem.DTO.Subject;
using StudentManagementSystem.Models.Entities;

namespace StudentManagementSystem.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public StudentController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetStudentDTO>>> GetStudent()
        {
            var students = await _dbContext.Students
                .Include(x => x.StudentSubjects)
                    .ThenInclude(x => x.Subject)
                .Select(s => new GetStudentDTO
                {
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    DateOfBirth = s.DateOfBirth,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber,
                    Address = s.Address,
                    Subjects = s.StudentSubjects.Select(x => new GetSubjectDTO
                    {
                        Id = x.Id,
                        Name = x.Subject.Name,
                        Code = x.Subject.Code
                    }).ToList()
                })
                .AsNoTracking()
                .ToListAsync();

            return Ok(students);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<GetStudentDTO>> GetStudentById(Guid id)
        {
            var student = await _dbContext.Students
                .Include(x => x.StudentSubjects)
                    .ThenInclude(x => x.Subject)
                .Where(s => s.Id == id)
                .Select(s => new GetStudentDTO
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    DateOfBirth = s.DateOfBirth,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber,
                    Address = s.Address,
                    Subjects = s.StudentSubjects.Select(x => new GetSubjectDTO
                    {
                        Id = x.Id,
                        Name = x.Subject.Name,
                        Code = x.Subject.Code
                    }).ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(AddUpdateStudent addStudentDto)
        {
            // Validate max 6 subjects
            if (addStudentDto.SubjectIds != null && addStudentDto.SubjectIds.Count() > 6)
            {
                return BadRequest("A student cannot have more than 6 subjects.");
            }

            var studentEntity = new Student()
            {
                Id = Guid.NewGuid(),
                FirstName = addStudentDto.FirstName,
                LastName = addStudentDto.LastName,
                Email = addStudentDto.Email,
                PhoneNumber = addStudentDto.PhoneNumber,
                DateOfBirth = addStudentDto.DateOfBirth,
                Address = addStudentDto.Address
            };

            _dbContext.Students.Add(studentEntity);
            await _dbContext.SaveChangesAsync();

            // Add student subjects
            if (addStudentDto.SubjectIds != null && addStudentDto.SubjectIds.Any())
            {
                var studentSubjects = addStudentDto.SubjectIds.Select(subjectId => new StudentSubject
                {
                    Id = Guid.NewGuid(),
                    StudentId = studentEntity.Id,
                    SubjectId = subjectId
                }).ToList();

                _dbContext.StudentSubjects.AddRange(studentSubjects);
                await _dbContext.SaveChangesAsync();
            }

            return Ok(new StudentResponse(true, "Student added successfully."));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudents(Guid id, AddUpdateStudent addStudentDto)
        {
            var record = await _dbContext.Students.FindAsync(id);

            if (record == null)
            {
                return NotFound(new StudentResponse(false, "Student not found."));
            }

            // Validate max 6 subjects
            if (addStudentDto.SubjectIds != null && addStudentDto.SubjectIds.Count() > 6)
            {
                return BadRequest("A student cannot have more than 6 subjects.");
            }

            record.FirstName = addStudentDto.FirstName;
            record.LastName = addStudentDto.LastName;
            record.Email = addStudentDto.Email;
            record.PhoneNumber = addStudentDto.PhoneNumber;
            record.DateOfBirth = addStudentDto.DateOfBirth;
            record.Address = addStudentDto.Address;

            _dbContext.Students.Update(record);
            await _dbContext.SaveChangesAsync();

            var existingSubjects = await _dbContext.StudentSubjects
                .Where(ss => ss.StudentId == id)
                .ToListAsync();

            var existingSubjectIds = existingSubjects.Select(ss => ss.SubjectId).ToHashSet();
            var incomingSubjectIds = addStudentDto.SubjectIds?.ToHashSet() ?? new HashSet<Guid>();

            // Remove subjects that are not in the new list
            var subjectsToRemove = existingSubjects
                .Where(ss => !incomingSubjectIds.Contains(ss.SubjectId))
                .ToList();
            _dbContext.StudentSubjects.RemoveRange(subjectsToRemove);

            // Add new subjects not already assigned
            var subjectsToAdd = incomingSubjectIds
                .Where(subjectId => !existingSubjectIds.Contains(subjectId))
                .Select(subjectId => new StudentSubject
                {
                    Id = Guid.NewGuid(),
                    StudentId = id,
                    SubjectId = subjectId
                }).ToList();
            _dbContext.StudentSubjects.AddRange(subjectsToAdd);

            await _dbContext.SaveChangesAsync();

            return Ok(new StudentResponse(true, "Student updated successfully."));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudents(Guid id)
        {
            var record = await _dbContext.Students.FindAsync(id);

            if (record == null)
            {
                return NotFound(new StudentResponse(false, "Student not found."));
            }

            _dbContext.Students.Remove(record);
            await _dbContext.SaveChangesAsync();

            return Ok(new StudentResponse(true, "Student deleted successfully."));
        }
    }
}
