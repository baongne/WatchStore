using EasyBuy_Backend.Models;
namespace EasyBuy_Backend.Repositories.ProductRepo;

public interface IProductRepository : IRepository<Product>
{
	Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);
}
    

