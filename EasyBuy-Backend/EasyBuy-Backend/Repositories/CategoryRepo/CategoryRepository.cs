using EasyBuy_Backend.Data;
using EasyBuy_Backend.Models;
using EasyBuy_Backend.Repositories.UserRepo;

namespace EasyBuy_Backend.Repositories.CategoryRepo
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
		private readonly MyDbContext _context;

		public CategoryRepository(MyDbContext context) : base(context)
        {
			_context = context;
		}

		public IEnumerable<Category> GetActiveCategories()
		{
			return _context.Categories
				.Where(c => c.Status == Models.Enums.CategoryStatus.ENABLE)
				.ToList();
		}
	}
}
