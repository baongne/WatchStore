using EasyBuy_Frontend_Admin.Models;

namespace EasyBuy_Frontend_Admin.Services.VoucherSvc
{
	public interface IVoucherService
	{
		Task<List<VoucherViewModel>> GetVouchersAsync();

		Task<bool> AddVoucherAsync(VoucherViewModel voucher);

		Task<VoucherViewModel> GetVoucherByIdAsync(int id);

		Task<bool> UpdateVoucherAsync(VoucherViewModel voucher);

		Task<bool> DeleteVoucherAsync(int id);

	}
}
