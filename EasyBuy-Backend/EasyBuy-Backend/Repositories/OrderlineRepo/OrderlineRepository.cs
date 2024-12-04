using EasyBuy_Backend.Data;
using EasyBuy_Backend.Models;
using EasyBuy_Backend.Repositories.VoucherRepo;

namespace EasyBuy_Backend.Repositories.OrderlineRepo
{
	public class OrderlineRepository : Repository<OrderLine>, IOrderlineRepository
	{
		private readonly MyDbContext _context;

		public OrderlineRepository(MyDbContext context) : base(context)
		{
			_context = context;

		}
	}
}
