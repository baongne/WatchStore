using EasyBuy_Backend.Models;
namespace EasyBuy_Backend.Repositories.DashRepo
{
	public interface IDashboardRepository
	{
		int GetTotalProducts();
		int GetTotalCategories();
		int GetTotalOrders();
		int GetTotalCustomers();
		int GetTotalInventoryVouchers();
	}
}
