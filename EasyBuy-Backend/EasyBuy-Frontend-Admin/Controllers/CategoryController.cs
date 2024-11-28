using EasyBuy_Frontend_Admin.Models;
using EasyBuy_Frontend_Admin.Services.CategorySvc;
using Microsoft.AspNetCore.Mvc;

namespace EasyBuy_Frontend_Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            List<CategoryViewModel> categories = await _categoryService.GetCategoriesAsync();

            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel category)
        {
            if (await _categoryService.AddCategoryAsync(category))
            {
                TempData["Success"] = "Thêm mới danh mục thành công.";
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "Đã có lỗi xảy ra";
            return View(category);
        }

        public async Task<IActionResult> Edit(int id)
        {
            CategoryViewModel category = await _categoryService.GetCategoryByIdAsync(id);

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryViewModel category)
        {
            if (await _categoryService.UpdateCategoryAsync(category))
            {
                TempData["Success"] = "Chỉnh sửa danh mục thành công.";
                return RedirectToAction(nameof(Index));
            }
            TempData["Error"] = "Đã có lỗi xảy ra";
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);

            return Ok();
        }
    }
}
