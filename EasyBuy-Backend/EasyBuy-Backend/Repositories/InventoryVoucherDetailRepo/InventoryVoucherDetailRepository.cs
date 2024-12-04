using EasyBuy_Backend.Data;
using EasyBuy_Backend.Models;
using EasyBuy_Backend.Repositories.InventoryVoucherRepo;

namespace EasyBuy_Backend.Repositories.InventoryVoucherDetailRepo
{
    public class InventoryVoucherDetailRepository : Repository<InventoryVoucherDetail>, IInventoryVoucherDetailRepository
    {
        private readonly MyDbContext _context;

        public InventoryVoucherDetailRepository(MyDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
