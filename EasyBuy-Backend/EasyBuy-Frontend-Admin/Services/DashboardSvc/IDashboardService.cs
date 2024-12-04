using EasyBuy_Frontend_Admin.Models;

namespace EasyBuy_Frontend_Admin.Services.DashboardSvc
{
	public interface IDashboardService
	{
		Task<DashboardViewModel> GetDashboardAsync();

	}
}
