using EasyBuy_Frontend_Admin.Models;
using EasyBuy_Frontend_Admin.Services.UserSvc;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace EasyBuy_Frontend_Admin.Controllers
{
	public class UserController : Controller
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<IActionResult> Index()
		{
			List<UserViewModel> users = await _userService.GetUsersAsync();

			return View(users);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(UserViewModel user)
		{            
            if (await _userService.AddUserAsync(user))
			{
                TempData["Success"] = "Thêm mới người dùng thành công.";
                return RedirectToAction(nameof(Index));
			}
			TempData["Error"] = "Đã có lỗi xảy ra";
            return View(user);
		}

		public async Task<IActionResult> Edit(int id)
		{
			UserViewModel user = await _userService.GetUserByIdAsync(id);

			return View(user);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(UserViewModel user)
		{
			if (await _userService.UpdateUserAsync(user))
			{
                TempData["Success"] = "Chỉnh sửa người dùng thành công.";
                return RedirectToAction(nameof(Index));
			}
            TempData["Error"] = "Đã có lỗi xảy ra";
            return View(user);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			await _userService.DeleteUserAsync(id);

			return Ok();
		}
	}
}
