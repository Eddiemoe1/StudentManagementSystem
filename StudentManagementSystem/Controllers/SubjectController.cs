using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.DTO.Subject;
using StudentManagementSystem.Models.Entities;

namespace StudentManagementSystem.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class SubjectController : ControllerBase
    {
        public readonly ApplicationDbContext _dbContext;
        public SubjectController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetSubjectDTO>>> GetSubjects()
        {
            var subjects = await _dbContext.Subjects
                .Select(s => new GetSubjectDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Code = s.Code
                })
                .AsNoTracking()
                .ToListAsync();

            return Ok(subjects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetSubjectDTO>> GetSubjects(Guid id)
        {
            var subject = await _dbContext.Subjects
                .Where(s => s.Id == id)
                .Select(s => new GetSubjectDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Code = s.Code
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return Ok(subject);
        }


        [HttpPost]
        public async Task<SubjectResponse> AddSubject(AddUpdateSubjectDTO addSubjectDto)
        {
            var subjectEntity = new Subject()
            {
                Id = Guid.NewGuid(), // Generate a new ID for the subject
                Name = addSubjectDto.Name,
                Code = addSubjectDto.Code
            };

            _dbContext.Subjects.Add(subjectEntity);
            await _dbContext.SaveChangesAsync();

            return new SubjectResponse(true, "Subject added successfully.");

        }


        [HttpPut("{id}")]
        public async Task<SubjectResponse> UpdateSubjects(AddUpdateSubjectDTO addSubjectDto, Guid id)
        {
            var record = await _dbContext.Subjects.FindAsync(id);

            if (record == null)
            {
                return new SubjectResponse(false, "Subject not found.");
            }

            record.Name = addSubjectDto.Name;
            record.Code = addSubjectDto.Code;

            _dbContext.Subjects.Update(record);
            await _dbContext.SaveChangesAsync();

            return new SubjectResponse(true, "Subject updated successfully.");
        }
        [HttpDelete("{id}")]
        public async Task<SubjectResponse> DeleteSubjects(Guid id)
        {
            var record = await _dbContext.Subjects.FindAsync(id);

            if (record == null)
            {
                return new SubjectResponse(false, "Subject not found.");
            }

            _dbContext.Subjects.Remove(record);
            await _dbContext.SaveChangesAsync();

            return new SubjectResponse(true, "Subject deleted successfully.");
        }
        
    }
}
