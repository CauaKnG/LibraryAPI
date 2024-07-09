using LibraryAPI.Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthController(IConfiguration configuration, IUsuarioRepository usuarioRepository)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
        }

        [HttpPost("authorize")]
        public async Task<IActionResult> Authorize([FromBody] UserAuthorization userAuth)
        {
            if (userAuth == null || userAuth.UserId <= 0)
            {
                return BadRequest("User ID is required.");
            }

            var usuario = await _usuarioRepository.GetByIdAsync(userAuth.UserId);
            if (usuario != null)
            {
                var token = GenerateJwtToken(usuario.Id);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }

        private string GenerateJwtToken(int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key must be configured."));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    public class UserAuthorization
    {
        public int UserId { get; set; }
    }
}
