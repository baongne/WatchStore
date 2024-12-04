using EasyBuy_Frontend_Admin.Models;
using EasyBuy_Frontend_Admin.Services.SupplierSvc;
using Microsoft.AspNetCore.Mvc;

namespace EasyBuy_Frontend_Admin.Controllers
{
    public class SupplierController : Controller
    {
		private readonly ISupplierService _supplierService;

		public SupplierController(ISupplierService supplierService)
		{
			_supplierService = supplierService;
		}

		public async Task<IActionResult> Index()
		{
			List<SupplierViewModel> suppliers = await _supplierService.GetSuppliersAsync();

			return View(suppliers);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(SupplierViewModel supplier)
		{
			if (await _supplierService.AddSupplierAsync(supplier))
			{
				TempData["Success"] = "Thêm mới nhà cung cấp thành công.";
				return RedirectToAction(nameof(Index));
			}
			TempData["Error"] = "Đã có lỗi xảy ra";
			return View(supplier);
		}

		public async Task<IActionResult> Edit(int id)
		{
			SupplierViewModel supplier = await _supplierService.GetSupplierByIdAsync(id);

			return View(supplier);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(SupplierViewModel supplier)
		{
			if (await _supplierService.UpdateSupplierAsync(supplier))
			{
				TempData["Success"] = "Chỉnh sửa nhà cung cấp thành công.";
				return RedirectToAction(nameof(Index));
			}
			TempData["Error"] = "Đã có lỗi xảy ra";
			return View(supplier);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			await _supplierService.DeleteSupplierAsync(id);

			return Ok();
		}
	}
}
