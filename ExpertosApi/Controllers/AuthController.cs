using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BackendExpertos.Contexts;
using BackendExpertos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// CAMBIA ESTO: De "ApiMovies.Controllers" a "ExpertosApi.Controllers"
namespace ExpertosApi.Controllers  // ← CORRIGE ESTA LÍNEA
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ExpertoContext _context;

        public AuthController(IConfiguration configuration, ExpertoContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            var usuario = _context.Users.FirstOrDefault(u => u.Username == login.Username && u.Password == login.Password);
            if (usuario == null)
            {
                return Unauthorized(new { message = "Credenciales inválidas" });
            }

            var token = GenerateToken(usuario);
            return Ok(new
            {
                token,
                username = usuario.Username,
                role = usuario.Role
            });
        }

        private string GenerateToken(User usuario)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings["Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.Username),
                new Claim(ClaimTypes.Role, usuario.Role),
                new Claim("id_user", usuario.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}