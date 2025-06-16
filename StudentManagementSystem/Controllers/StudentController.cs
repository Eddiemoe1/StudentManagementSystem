using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.DTO.Student;
using StudentManagementSystem.DTO.Subject;
using StudentManagementSystem.Models.Entities;


namespace StudentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class StudentController : Controller
    {
        public readonly ApplicationDbContext _dbContext;
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
                        Id = x.Subject.Id,
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
                        Id = x.Subject.Id,
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
        public async Task<StudentResponse> AddStudent(AddUpdateStudent addStudentDto)
        {
            var studentEntity = new Student()
            {
                Id = Guid.NewGuid(),
                FirstName = addStudentDto.FirstName,
                LastName = addStudentDto.LastName,
                Email = addStudentDto.Email,
                PhoneNumber = addStudentDto.PhoneNumber,
                DateOfBirth=addStudentDto.DateOfBirth,
                Address=addStudentDto.Address
            };

            _dbContext.Students.Add(studentEntity);
            await _dbContext.SaveChangesAsync();

            // Add student subjects
            var studentSubjects = addStudentDto.SubjectIds.Select(subjectId => new StudentSubject
            {
                Id = Guid.NewGuid(),
                StudentId = studentEntity.Id,
                SubjectId = subjectId
            }).ToList();

            _dbContext.StudentSubjects.AddRange(studentSubjects);

            await _dbContext.SaveChangesAsync();

            return new StudentResponse(true, "Student added successfully.");
        }

        [HttpPut]
        public async Task<StudentResponse> UpdateStudents(AddUpdateStudent addStudentDto, Guid id)
        {
            var record = await _dbContext.Students.FindAsync(id);

            if (record == null)
            {
                return new StudentResponse(false, "Student not found.");
            }

            record.FirstName = addStudentDto.FirstName;
            record.LastName = addStudentDto.LastName;
            record.Email = addStudentDto.Email;
            record.PhoneNumber = addStudentDto.PhoneNumber;

            _dbContext.Students.Update(record);
            await _dbContext.SaveChangesAsync();

          
            var existingSubjects = await _dbContext.StudentSubjects
                .Where(ss => ss.StudentId == id)
                .ToListAsync();

            var existingSubjectIds = existingSubjects.Select(ss => ss.SubjectId).ToHashSet();
            var incomingSubjectIds = addStudentDto.SubjectIds.ToHashSet();

            // If subjectid not in dto, delete
            var subjectsToRemove = existingSubjects
                .Where(ss => !incomingSubjectIds.Contains(ss.SubjectId))
                .ToList();
            _dbContext.StudentSubjects.RemoveRange(subjectsToRemove);

            //  If subject new, add
            var subjectsToAdd = incomingSubjectIds
                .Where(subjectId => !existingSubjectIds.Contains(subjectId))
                .Select(subjectId => new StudentSubject
                {
                    Id = Guid.NewGuid(),
                    StudentId = id,
                    SubjectId = subjectId
                }).ToList();
            _dbContext.StudentSubjects.AddRange(subjectsToAdd);

            // Save changes for subject updates
            await _dbContext.SaveChangesAsync();

            return new StudentResponse(true, "Student updated successfully.");
        }


        [HttpDelete]
        public async Task<StudentResponse> DeleteStudents(Guid id)
        {
            var record = await _dbContext.Students.FindAsync(id);

            if (record == null)
            {
                return new StudentResponse(false, "Student not found.");
            }

            _dbContext.Students.Remove(record);
            await _dbContext.SaveChangesAsync();

            return new StudentResponse(true, "Student deleted successfully.");
        }
    }
}
