using EasyBuy_Frontend_Admin.Dtos.Auth;
using EasyBuy_Frontend_Admin.Models;
using EasyBuy_Frontend_Admin.Services.AuthSvc;
using EasyBuy_Frontend_Admin.Services.UserSvc;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace EasyBuy_Frontend_Admin.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(
            IAuthService authService,
            IUserService userService
        ) {
            _authService = authService;
            _userService = userService;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> SignIn(SignInDTO signInDTO)
		{
			if (ModelState.IsValid)
			{
				var result = _authService.Login(signInDTO);

                UserViewModel user = await _userService.GetUserByEmailAsync(signInDTO.Email);

                if (result != null)
				{
					HttpContext.Session.SetString("IsAuthenticated", "True");

                    string userJson = JsonSerializer.Serialize(user);
                    HttpContext.Session.SetString("CurrentUser", userJson);

                    TempData["Success"] = "Đăng nhập thành công.";
					return RedirectToAction("Index", "Dashboard");  
				}

				ModelState.AddModelError("", "Đăng nhập không thành công.");
			}

			return View(signInDTO); 
		}

		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpDTO signUpDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.Register(signUpDTO);
                if (result != null)
                {
                    TempData["Success"] = "Đăng ký tài khoản thành công.";
                    return RedirectToAction("SignIn");
                }
                ModelState.AddModelError("", "Đăng ký không thành công.");
            }
            return View(signUpDTO);
        }

		[HttpPost]
		public IActionResult Logout()
		{
			HttpContext.Session.Remove("IsAuthenticated");

			return Json(new { success = true });
		}
        public async Task<IActionResult> ViewProfile()
        {
            string userJson = HttpContext.Session.GetString("CurrentUser");

            UserViewModel user = JsonSerializer.Deserialize<UserViewModel>(userJson);

            return View(user);
        }
        public async Task<IActionResult> Profile()
		{
			string userJson = HttpContext.Session.GetString("CurrentUser");

			UserViewModel user = JsonSerializer.Deserialize<UserViewModel>(userJson);

			return View(user);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Profile(UserViewModel user)
		{
			if (await _userService.UpdateUserAsync(user))
			{
				TempData["Success"] = "Chỉnh sửa hồ sơ thành công.";
				return View(user);
			}
			TempData["Error"] = "Đã có lỗi xảy ra";
			return View(user);
		}
	}
}

