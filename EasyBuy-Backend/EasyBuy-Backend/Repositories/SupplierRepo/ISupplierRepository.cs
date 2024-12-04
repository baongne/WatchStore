using EasyBuy_Backend.Models;

namespace EasyBuy_Backend.Repositories.SupplierRepo
{
	public interface ISupplierRepository : IRepository<Supplier>
	{
		IEnumerable<Supplier> GetActiveSuppliers();
	}
}
