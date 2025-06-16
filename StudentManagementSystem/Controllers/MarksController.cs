using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Data;
using StudentManagementSystem.DTO.Marks;
using Microsoft.EntityFrameworkCore;





namespace StudentManagementSystem.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MarksController : Controller
    {
            public readonly ApplicationDbContext _dbContext;
            public MarksController(ApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            //[HttpGet]
            //public async Task<ActionResult<List<GetMarksDTO>>> Getmarks()
            //{
            //    var marks = await _dbContext.Marks
            //    .Select(s => new GetMarksDTO
            //    {
            //        StudentSubject = s.StudentSubject,
            //        TotalMarks = s.TotalMarks

            //    })
            //        .AsNoTracking()
            //        .ToListAsync()
            //        ;

            //    return Ok(marks);

            //}

        
    }
}
