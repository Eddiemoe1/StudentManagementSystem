using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.DTO.Marks;
using StudentManagementSystem.Models.Entities;


namespace StudentManagementSystem.Controllers
{

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class MarksController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public MarksController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetMarksDTO>>> GetMarks()
        {
            var marks = await _dbContext.Marks
                .Include(m => m.StudentSubject)
                    .ThenInclude(ss => ss.Subject)
                .Include(m => m.StudentSubject)
                    .ThenInclude(ss => ss.Student)
                .Select(m => new GetMarksDTO
                {
                    Id = m.Id,
                    StudentSubjectId = m.StudentSubjectId,
                    TotalMark = m.TotalMark,
                    StudentName = m.StudentSubject.Student.FirstName + " " + m.StudentSubject.Student.LastName,
                    SubjectName = m.StudentSubject.Subject.Name
                })
                .ToListAsync();

            return Ok(marks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetMarksDTO>> GetMarkById(Guid id)
        {
            var mark = await _dbContext.Marks
                .Include(m => m.StudentSubject)
                    .ThenInclude(ss => ss.Subject)
                .Include(m => m.StudentSubject)
                    .ThenInclude(ss => ss.Student)
                .Where(m => m.Id == id)
                .Select(m => new GetMarksDTO
                {
                    Id = m.Id,
                    StudentSubjectId = m.StudentSubjectId,
                    TotalMark = m.TotalMark,
                    StudentName = m.StudentSubject.Student.FirstName + " " + m.StudentSubject.Student.LastName,
                    SubjectName = m.StudentSubject.Subject.Name
                })
                .FirstOrDefaultAsync();

            if (mark == null)
                return NotFound();

            return Ok(mark);
        }

        [HttpPost]
        public async Task<IActionResult> AddMark(CreateMarksDTO addMarkDto)
        {
            // Validate mark range
            if (addMarkDto.TotalMark < 0 || addMarkDto.TotalMark > 100)
                return BadRequest("TotalMark must be between 0 and 100.");

            // Check if StudentSubject exists
            var studentSubjectExists = await _dbContext.StudentSubjects.AnyAsync(ss => ss.Id == addMarkDto.StudentSubjectId);
            if (!studentSubjectExists)
                return BadRequest("StudentSubjectId is invalid.");

            // Prevent duplicate mark entry for same StudentSubject
            var duplicateMark = await _dbContext.Marks.AnyAsync(m => m.StudentSubjectId == addMarkDto.StudentSubjectId);
            if (duplicateMark)
                return BadRequest("Mark for this StudentSubject already exists.");

            var mark = new Mark
            {
                Id = Guid.NewGuid(),
                StudentSubjectId = addMarkDto.StudentSubjectId,
                TotalMark = addMarkDto.TotalMark
            };

            _dbContext.Marks.Add(mark);
            await _dbContext.SaveChangesAsync();

            return Ok(mark);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMark(Guid id, CreateMarksDTO updateMarkDto)
        {
            var mark = await _dbContext.Marks.FindAsync(id);
            if (mark == null)
                return NotFound();

            // Validate mark range
            if (updateMarkDto.TotalMark < 0 || updateMarkDto.TotalMark > 100)
                return BadRequest("TotalMark must be between 0 and 100.");

            // Check if StudentSubject exists
            var studentSubjectExists = await _dbContext.StudentSubjects.AnyAsync(ss => ss.Id == updateMarkDto.StudentSubjectId);
            if (!studentSubjectExists)
                return BadRequest("StudentSubjectId is invalid.");

            // Check for duplicates if StudentSubjectId changed
            if (mark.StudentSubjectId != updateMarkDto.StudentSubjectId)
            {
                var duplicateMark = await _dbContext.Marks.AnyAsync(m => m.StudentSubjectId == updateMarkDto.StudentSubjectId);
                if (duplicateMark)
                    return BadRequest("Mark for this StudentSubject already exists.");
            }

            mark.StudentSubjectId = updateMarkDto.StudentSubjectId;
            mark.TotalMark = updateMarkDto.TotalMark;

            _dbContext.Marks.Update(mark);
            await _dbContext.SaveChangesAsync();

            return Ok(mark);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMark(Guid id)
        {
            var mark = await _dbContext.Marks.FindAsync(id);
            if (mark == null)
                return NotFound();

            _dbContext.Marks.Remove(mark);
            await _dbContext.SaveChangesAsync();

            return Ok("Mark deleted successfully.");
        }
    }
}
