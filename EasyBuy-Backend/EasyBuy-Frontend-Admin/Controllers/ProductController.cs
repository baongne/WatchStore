using EasyBuy_Frontend_Admin.Models;
using EasyBuy_Frontend_Admin.Services.CategorySvc;
using EasyBuy_Frontend_Admin.Services.ProductSvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics; 

namespace EasyBuy_Frontend_Admin.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductService _productService;
		private readonly ICategoryService _categoryService;

		public ProductController(
			IProductService productService, 
			ICategoryService categoryService
		) {
			_productService = productService;
			_categoryService = categoryService;
		}

		public async Task<IActionResult> Index()
		{
			List<ProductViewModel> products = await _productService.GetProductsAsync();
			List<CategoryViewModel> categories = await _categoryService.GetCategoriesAsync();
			foreach (var product in products)
			{
				product.CategoryName = categories.FirstOrDefault(c => c.Id == product.CategoryId)?.Name;
                product.Discount *= 100;
            }
			return View(products);
		}

		public async Task<IActionResult> Create()
		{
			List<CategoryViewModel> categories = await _categoryService.GetCategoriesActive();
			string highestCode = await _productService.GetHighestCodeAsync();
			if (string.IsNullOrEmpty(highestCode))
			{
				highestCode = "DH - 001";
			}
			else
			{
				var parts = highestCode.Split(" - ");
				if (parts.Length == 2 && int.TryParse(parts[1], out int number))
				{
					highestCode = $"{parts[0]} - {number + 1:D3}"; 
				}
			}
			ViewBag.HighestCode = highestCode;
			ViewBag.Categories = new SelectList(categories, "Id", "Name");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ProductViewModel product)
		{
			List<CategoryViewModel> categories = await _categoryService.GetCategoriesActive();
			string highestCode = await _productService.GetHighestCodeAsync();
			if (string.IsNullOrEmpty(highestCode))
			{
				highestCode = "DH - 001";
			}
			else
			{
				var parts = highestCode.Split(" - ");
				if (parts.Length == 2 && int.TryParse(parts[1], out int number))
				{
					highestCode = $"{parts[0]} - {number + 1:D3}";
				}
			}
			
			if (string.IsNullOrEmpty(product.ProductImg))
			{
				ViewBag.HighestCode = highestCode;
				ViewBag.Categories = new SelectList(categories, "Id", "Name");
				return View(product);
			}
			string uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
			string filePath = Path.Combine(uploadDirectory, product.ProductImg);
			if (System.IO.File.Exists(filePath))
			{
				product.ProductImg = $"/images/{product.ProductImg}";	
			}
			else
			{
				TempData["Error"] = "File không tồn tại. Vui lòng thử lại.";
				ViewBag.HighestCode = highestCode;
				ViewBag.Categories = new SelectList(categories, "Id", "Name");
				return View(product);
			}
			
			if (await _productService.AddProductAsync(product))
			{
				TempData["Success"] = "Thêm mới sản phẩm thành công.";
				ViewBag.HighestCode = highestCode;
				ViewBag.Categories = new SelectList(categories, "Id", "Name");
				return RedirectToAction(nameof(Index));
			}
			TempData["Error"] = "Đã có lỗi xảy ra.";
			ViewBag.HighestCode = highestCode;
			ViewBag.Categories = new SelectList(categories, "Id", "Name");
			return View(product);
		}

		public async Task<IActionResult> Edit(int id)
		{
			ProductViewModel product = await _productService.GetProductByIdAsync(id);
			List<CategoryViewModel> categories = await _categoryService.GetCategoriesActive();
			ViewBag.Categories = new SelectList(categories, "Id", "Name");
			product.Discount *= 100;
			return View(product);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(ProductViewModel product)
		{
			List<CategoryViewModel> categories = await _categoryService.GetCategoriesActive();
			product.ProductImg = $"/images/{product.ProductImg}";

			// Kiểm tra ảnh sản phẩm
			if (string.IsNullOrEmpty(product.ProductImg))
			{
				ViewBag.Categories = new SelectList(categories, "Id", "Name");
				return View(product);
			}

			string uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
			string filePath = Path.Combine(uploadDirectory, product.ProductImg);
			// Cập nhật sản phẩm
			if (await _productService.UpdateProductAsync(product))
			{
				TempData["Success"] = "Chỉnh sửa sản phẩm thành công.";
				return RedirectToAction(nameof(Index));
			}

			// Nếu có lỗi trong quá trình cập nhật
			TempData["Error"] = "Đã có lỗi xảy ra";
			ViewBag.Categories = new SelectList(categories, "Id", "Name");
			return View(product);
		}


		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			await _productService.DeleteProductAsync(id);

			return Ok();
		}


		[HttpGet]
		public JsonResult GetProducts(int id)
		{
			var category = _categoryService.GetCategoryByIdAsync(id);
			if (category == null)
			{
				return Json(new { success = false, message = "Danh mục không tồn tại" });
			}

			var products = _productService.GetProductsByCategoryAsync(id);

			return Json(new { success = true, products = products });
		}
	}
}
