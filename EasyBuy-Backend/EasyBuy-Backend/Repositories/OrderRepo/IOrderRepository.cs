using EasyBuy_Backend.Dtos.Order;
using EasyBuy_Backend.Models;

namespace EasyBuy_Backend.Repositories.OrderRepo;

public interface IOrderRepository : IRepository<Order>
{
	Task<List<Order>> GetAllAsync();

    Task<Order> GetOrderById(int id);

    Task<bool> UpdateOrderStatusAsync(Order order, UpdateOrderDTO updateOrderDTO);

	Task<Order> CreateOrderAsync(Order order);

	Task<Dictionary<string, int>> GetOrderStatisticsAsync();

}
