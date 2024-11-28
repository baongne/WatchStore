using EasyBuy_Backend.Models;

namespace EasyBuy_Backend.Repositories.InventoryVoucherRepo;

public interface IInventoryVoucherRepository : IRepository<InventoryVoucher>
{
    Task<List<InventoryVoucher>> GetAllAsync();

    Task<InventoryVoucher> GetInventoryVoucherById(int id);
}
