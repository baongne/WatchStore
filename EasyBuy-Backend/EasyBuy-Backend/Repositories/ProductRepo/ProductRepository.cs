using EasyBuy_Backend.Data;
using EasyBuy_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EasyBuy_Backend.Repositories.ProductRepo;
public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly MyDbContext _context;

    public ProductRepository(MyDbContext context) : base(context)
    {
        _context = context;
    }

	public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
	{
		var products = await _context.Products
									  .Where(p => p.CategoryId == categoryId)
									  .ToListAsync();
		return products;
	}
}
