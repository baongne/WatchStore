using EasyBuy_Backend.Data;
using EasyBuy_Backend.Models;
using EasyBuy_Backend.Repositories.SupplierRepo;
using Microsoft.EntityFrameworkCore;

namespace EasyBuy_Backend.Repositories.VoucherRepo
{
	public class VoucherRepository : Repository<Voucher>, IVoucherRepository
	{
		private readonly MyDbContext _context;

		public VoucherRepository(MyDbContext context) : base(context)
		{
			_context = context;

		}
		
	}
}
