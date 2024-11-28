using EasyBuy_Backend.Models;

namespace EasyBuy_Backend.Repositories.CategoryRepo
{
    public interface ICategoryRepository : IRepository<Category>
    {
		IEnumerable<Category> GetActiveCategories();
	}
}
