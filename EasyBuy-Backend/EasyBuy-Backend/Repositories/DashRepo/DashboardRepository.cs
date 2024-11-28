using EasyBuy_Backend.Data;
using EasyBuy_Backend.Models;
using EasyBuy_Backend.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace EasyBuy_Backend.Repositories.DashRepo
{
	public class DashboardRepository : IDashboardRepository
	{
		private readonly MyDbContext _context;

		public DashboardRepository(MyDbContext context)
		{
			_context = context;
		}

		public int GetTotalProducts()
		{
			return _context.Products.Count(); 
		}

		public int GetTotalCategories()
		{
			return _context.Categories.Count(); 
		}

		public int GetTotalOrders()
		{
			return _context.Orders.Count(); 
		}

		public int GetTotalCustomers()
		{
			return _context.Users.Count(u => u.Role == UserRole.USER);
		}


		public int GetTotalInventoryVouchers()
		{
			return _context.InventoryVouchers.Count();
		}
	}
}
