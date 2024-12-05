using EasyBuy_Frontend_Admin.Models;
using EasyBuy_Frontend_Admin.Services.DashboardSvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Text.Json;

namespace EasyBuy_Frontend_Admin.Controllers
{
    public class DashboardController : Controller
    {
		private readonly IDashboardService _dashboardService;

		public DashboardController(
			IDashboardService dashboardService
		)
		{
			_dashboardService = dashboardService;
		}
		public async Task<IActionResult> Index()
		{
            string userJson = HttpContext.Session.GetString("CurrentUser");

            if (!string.IsNullOrEmpty(userJson))
            {
                UserViewModel user = JsonSerializer.Deserialize<UserViewModel>(userJson);
            }
            else
            {
                Debug.WriteLine("Session không chứa thông tin người dùng.");
            }

            DashboardViewModel dashboards = await _dashboardService.GetDashboardAsync();
			return View(dashboards);
		}
    }
}
