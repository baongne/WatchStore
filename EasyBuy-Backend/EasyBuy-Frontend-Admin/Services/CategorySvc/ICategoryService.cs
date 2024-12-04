using EasyBuy_Frontend_Admin.Models;

namespace EasyBuy_Frontend_Admin.Services.CategorySvc
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetCategoriesAsync();

        Task<bool> AddCategoryAsync(CategoryViewModel category);

        Task<CategoryViewModel> GetCategoryByIdAsync(int id);

        Task<bool> UpdateCategoryAsync(CategoryViewModel category);

        Task<bool> DeleteCategoryAsync(int id);

        Task<List<CategoryViewModel>> GetCategoriesActive();

	}
}
