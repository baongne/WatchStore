using EasyBuy_Frontend_Admin.Dtos.Order;
using EasyBuy_Frontend_Admin.Models;

namespace EasyBuy_Frontend_Admin.Services.OrderSvc
{
	public interface IOrderService
	{
		Task<List<OrderViewModel>> GetOrderiesAsync();

		Task<bool> AddOrderAsync(OrderViewModel order);

		Task<OrderViewModel> GetOrderByIdAsync(int id);

		Task<bool> UpdateOrderAsync(UpdateOrderDTO order);

		Task<bool> DeleteOrderAsync(int id);
	}
}
