using Microsoft.AspNetCore.Mvc;
using EasyBuy_Backend.Models;
using EasyBuy_Backend.Repositories.DashRepo;

namespace EasyBuy_Backend.Controllers
{
		[ApiController]
		[Route("api/[controller]")]
		public class DashboardController : ControllerBase
		{
			private readonly IDashboardRepository _dashboardRepository;

			public DashboardController(IDashboardRepository dashboardRepository)
			{
				_dashboardRepository = dashboardRepository;
			}

			[HttpGet("statistics")]
			public IActionResult GetStatistics()
			{
				var statistics = new
				{
					TotalProducts = _dashboardRepository.GetTotalProducts(),
					TotalCategories = _dashboardRepository.GetTotalCategories(),
					TotalOrders = _dashboardRepository.GetTotalOrders(),
					TotalCustomers = _dashboardRepository.GetTotalCustomers(),
					TotalInventoryVouchers = _dashboardRepository.GetTotalInventoryVouchers()
				};

				return Ok(statistics);
			}
		}

	}