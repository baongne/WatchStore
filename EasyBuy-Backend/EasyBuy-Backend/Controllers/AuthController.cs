using EasyBuy_Backend.Dtos.Auth;
using EasyBuy_Backend.Models;
using EasyBuy_Backend.Services.AuthSvc;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EasyBuy_Backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("Register")]
		public async Task<IActionResult> Register([FromBody] User user)
		{
			var token = await _authService.Register(user);
			if (!string.IsNullOrEmpty(token))
			{
				return Ok(new { Token = token }); 
			}

			return BadRequest(new { message = "Đăng ký không thành công." });
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] SignInDTO signInDTO)
		{
			var token = _authService.Login(signInDTO.Email, signInDTO.Password);
			if (!string.IsNullOrEmpty(token))
			{
				return Ok(new { Token = token });
			}

			return Unauthorized(new { message = "Email hoặc mật khẩu không đúng." });
		}

	}
}
