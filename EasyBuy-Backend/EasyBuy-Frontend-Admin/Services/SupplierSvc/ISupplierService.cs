using EasyBuy_Frontend_Admin.Models;

namespace EasyBuy_Frontend_Admin.Services.SupplierSvc
{
    public interface ISupplierService
    {
        Task<List<SupplierViewModel>> GetSuppliersAsync();

        Task<bool> AddSupplierAsync(SupplierViewModel supplier);

        Task<SupplierViewModel> GetSupplierByIdAsync(int id);

        Task<bool> UpdateSupplierAsync(SupplierViewModel supplier);

        Task<bool> DeleteSupplierAsync(int id);

        Task<List<SupplierViewModel>> GetSuppliersActive();

	}
}
