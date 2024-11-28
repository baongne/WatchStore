using EasyBuy_Frontend_Admin.Models;

namespace EasyBuy_Frontend_Admin.Services.ProductSvc
{
    public interface IProductService
    {
        Task<List<ProductViewModel>> GetProductsAsync();
		Task<bool> AddProductAsync(ProductViewModel product);

		Task<ProductViewModel> GetProductByIdAsync(int id);

		Task<bool> UpdateProductAsync(ProductViewModel product);

		Task<bool> DeleteProductAsync(int id);

		Task<string> GetHighestCodeAsync();

		Task<List<ProductViewModel>> GetProductsByCategoryAsync(int categoryId);
	}
}
