using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.DTO.Staff;
using StudentManagementSystem.Models.Entities;

namespace StudentManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StaffsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Staffs
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.Staffs
                .Select(s => new StaffDto {
                    Id = s.Id,
                    StaffId = s.StaffId,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.Email,
                    Phone = s.Phone,
                    Department = s.Department,
                    Position = s.Position,
                    HireDate = s.HireDate,
                    Role = s.Role,
                    Status = s.Status
                })
                .ToListAsync();

            return Ok(list);
        }

        // GET: api/Staffs/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var s = await _context.Staffs.FindAsync(id);
            if (s == null) return NotFound();

            var dto = new StaffDto {
                Id = s.Id,
                StaffId = s.StaffId,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                Phone = s.Phone,
                Department = s.Department,
                Position = s.Position,
                HireDate = s.HireDate,
                Role = s.Role,
                Status = s.Status
            };

            return Ok(dto);
        }

        // POST: api/Staffs
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStaffDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var staff = new Staff {
                Id = Guid.NewGuid().ToString(),
                StaffId = string.IsNullOrWhiteSpace(dto.StaffId) ? "STF" + DateTime.UtcNow.Ticks.ToString().Substring(0,6) : dto.StaffId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                Department = dto.Department,
                Position = dto.Position,
                HireDate = dto.HireDate ?? DateTime.UtcNow,
                Role = dto.Role,
                Status = dto.Status
            };

            _context.Staffs.Add(staff);
            await _context.SaveChangesAsync();

            var resultDto = new StaffDto {
                Id = staff.Id,
                StaffId = staff.StaffId,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                Email = staff.Email,
                Phone = staff.Phone,
                Department = staff.Department,
                Position = staff.Position,
                HireDate = staff.HireDate,
                Role = staff.Role,
                Status = staff.Status
            };

            return CreatedAtAction(nameof(GetById), new { id = staff.Id }, resultDto);
        }

        // PUT: api/Staffs/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateStaffDto dto)
        {
            if (id != dto.Id) return BadRequest("Id mismatch.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var staff = await _context.Staffs.FindAsync(id);
            if (staff == null) return NotFound();

            // update fields
            staff.StaffId = dto.StaffId;
            staff.FirstName = dto.FirstName;
            staff.LastName = dto.LastName;
            staff.Email = dto.Email;
            staff.Phone = dto.Phone;
            staff.Department = dto.Department;
            staff.Position = dto.Position;
            staff.HireDate = dto.HireDate;
            staff.Role = dto.Role;
            staff.Status = dto.Status;

            _context.Staffs.Update(staff);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Staffs/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var staff = await _context.Staffs.FindAsync(id);
            if (staff == null) return NotFound();

            _context.Staffs.Remove(staff);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
