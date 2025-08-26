using Agro.API.Entidades;
using Agro.Entidades;
using Agro.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Agro.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogEventoService _logService;

        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration, ILogger<AuthController> logger, ILogEventoService logService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
            _logService = logService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                NomeCompleto = model.NomeCompleto
            };

            var result = await _userManager.CreateAsync(user, model.Senha);

            if (result.Succeeded)
                return Ok(new { message = "Usuário registrado com sucesso." });

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email.Trim());

            if (user == null)
            {
                // Salvar log de sucesso
                await _logService.SalvarLogAsync(new LogEvento
                {
                    Level = "Information",
                    Message = $"Usuário {model.Email} autenticado com sucesso."
                });

                return Unauthorized();
            }

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Senha))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    expires: DateTime.Now.AddHours(2),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                        SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            if (!await _userManager.CheckPasswordAsync(user, model.Senha))
            {

                await _logService.SalvarLogAsync(new LogEvento
                {
                    Level = "Warning",
                    Message = $"Falha na autenticação para o email {model.Email}."
                });
            }

            return Unauthorized();
        }
    }
}
