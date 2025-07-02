using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SE4458FinalProject.Services;
using SE4458FinalProject.Models;

namespace SE4458FinalProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            user.Id = null; // id alanı gönderilse bile sıfırlanır
            var existing = await _userService.GetByEmailAsync(user.Email);
            if (existing != null)
                return BadRequest(new { message = "Bu email ile kayıtlı kullanıcı var." });
            await _userService.CreateAsync(user);
            return Ok(new { message = "Kayıt başarılı!" });
        }
    }
} 