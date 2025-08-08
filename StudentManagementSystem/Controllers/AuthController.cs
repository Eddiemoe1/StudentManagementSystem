using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Asp.Versioning;
using StudentManagementSystem.DTO.Auth;

namespace StudentManagementSystem.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO model)
        {
            var password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            if (model.Password != model.ConfirmPassword)
           return BadRequest("Passwords do not match.");

            var user = new User
                {
                    Email = model.Email,
                    UserName = model.FirstName + " " + model.LastName,
                    Password = password,
                    Role = model.Role,
                    StudentOrStaffNo = model.Id
                };


            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (existingUser != null)
                return BadRequest("Check your credentials.");

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
public async Task<IActionResult> Login([FromBody] LoginRequest request)
{
    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

    if (user == null)
    {
        return Unauthorized(new { message = "Email not found" });
    }

    if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
    {
        return Unauthorized(new { message = "Password mismatch" });
    }

    var token = GenerateJwtToken(user);

    return Ok(new
    {
        token,
        user = new
        {
            id = user.Id,
            email = user.Email,
            username = user.UserName,
            role = user.Role,
        }
    });
}


        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _config.GetSection("JwtSettings");

            var keyString = jwtSettings["Key"]
                ?? throw new InvalidOperationException("JWT key is not configured in appsettings.");

            var issuer = jwtSettings["Issuer"]
                ?? throw new InvalidOperationException("JWT issuer is not configured.");

            var audience = jwtSettings["Audience"]
                ?? throw new InvalidOperationException("JWT audience is not configured.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("StudentOrStaffNo", user.StudentOrStaffNo)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
