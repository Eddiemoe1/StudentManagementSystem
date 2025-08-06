using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using YourNamespace.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StaffController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/staff
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staff>>> GetStaff()
        {
            return await _context.Staff.ToListAsync();
        }

        // GET: api/staff/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Staff>> GetStaff(string id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff == null)
                return NotFound();

            return staff;
        }

        // POST: api/staff
        [HttpPost]
        public async Task<ActionResult<Staff>> CreateStaff(Staff staff)
        {
            if (staff == null) return BadRequest();

            staff.Id = Guid.NewGuid().ToString(); // Assign unique ID
            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStaff), new { id = staff.Id }, staff);
        }

        // PUT: api/staff/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff(string id, Staff updatedStaff)
        {
            if (id != updatedStaff.Id)
                return BadRequest("ID mismatch");

            _context.Entry(updatedStaff).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Staff.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/staff/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(string id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff == null)
                return NotFound();

            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
