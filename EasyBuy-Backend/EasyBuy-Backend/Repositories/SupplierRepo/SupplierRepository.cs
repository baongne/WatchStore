using EasyBuy_Backend.Data;
using EasyBuy_Backend.Models;

namespace EasyBuy_Backend.Repositories.SupplierRepo
{
	public class SupplierRepository : Repository<Supplier>, ISupplierRepository
	{
		private readonly MyDbContext _context;

		public SupplierRepository(MyDbContext context) : base(context)
		{
			_context = context;
		}

		public IEnumerable<Supplier> GetActiveSuppliers()
		{
			return _context.Suppliers
				.Where(c => c.Status == Models.Enums.SupplierStatus.ENABLE)
				.ToList();
		}
	}
}
