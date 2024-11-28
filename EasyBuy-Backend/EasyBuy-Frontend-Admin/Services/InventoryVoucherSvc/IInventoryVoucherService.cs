using EasyBuy_Frontend_Admin.Dtos.InventoryVoucherDetail;
using EasyBuy_Frontend_Admin.Models;

namespace EasyBuy_Frontend_Admin.Services.InventoryVoucherSvc
{
	public interface IInventoryVoucherService
	{
		Task<List<InventoryVoucherViewModel>> GetInventoryVoucheriesAsync();

		Task<InventoryVoucherViewModel> GetInventoryVoucherByIdAsync(int id);

		Task<bool> UpdateInventoryVoucherAsync(InventoryVoucherViewModel inventoryvoucher);

		Task<bool> DeleteInventoryVoucherAsync(int id);

        bool AddInventoryVoucher(List<InventoryVoucherDetailDTO> details);

    }
}
