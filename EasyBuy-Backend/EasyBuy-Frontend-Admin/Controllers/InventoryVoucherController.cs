using EasyBuy_Frontend_Admin.Dtos.InventoryVoucherDetail;
using EasyBuy_Frontend_Admin.Models;
using EasyBuy_Frontend_Admin.Services.InventoryVoucherSvc;
using EasyBuy_Frontend_Admin.Services.ProductSvc;
using EasyBuy_Frontend_Admin.Services.SupplierSvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Diagnostics;

namespace EasyBuy_Frontend_Admin.Controllers
{
	public class InventoryVoucherController : Controller
	{
		private readonly IInventoryVoucherService _inventoryvoucherService;
		private readonly ISupplierService _supplierService;
		private readonly IProductService _productService;

		public InventoryVoucherController(
			IProductService productService,
			IInventoryVoucherService inventoryVoucherService,
			ISupplierService supplierService
		) {
			_inventoryvoucherService = inventoryVoucherService;
			_supplierService = supplierService;
			_productService = productService;
		}

		public async Task<IActionResult> Index()
		{
			List<InventoryVoucherViewModel> inventoryvoucheries = await _inventoryvoucherService.GetInventoryVoucheriesAsync();

			return View(inventoryvoucheries);
		}

		public async Task<IActionResult> Create()
		{
			List<SupplierViewModel> suppliers = await _supplierService.GetSuppliersActive();
			List<ProductViewModel> products = await _productService.GetProductsAsync();

			ViewBag.Suppliers = new SelectList(suppliers, "Id", "Name");
			ViewBag.Products = products;

			return View();
		}

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] List<InventoryVoucherDetailDTO> inventoryvoucher)
        {
            try
            {
                var result = _inventoryvoucherService.AddInventoryVoucher(inventoryvoucher);

                return Json(new { success = true, message = "Taọ mới phiếu nhập thành công" });
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Lỗi: " + ex.Message);
                return Json(new { success = false, message = "Đã có lỗi xảy ra trong quá trình xử lý dữ liệu." });
            }
        }

        public async Task<IActionResult> Show(int id)
		{
			InventoryVoucherViewModel inventoryvoucher = await _inventoryvoucherService.GetInventoryVoucherByIdAsync(id);

			List<SupplierViewModel> suppliers = await _supplierService.GetSuppliersActive();

			return View(inventoryvoucher);
		}

		//[HttpPost]
		//[ValidateAntiForgeryToken]
		//public async Task<IActionResult> Edit(InventoryVoucherViewModel inventoryvoucher)
		//{
		//	if (await _inventoryvoucherService.UpdateInventoryVoucherAsync(inventoryvoucher))
		//	{
		//		TempData["Success"] = "Chỉnh sửa phiếu nhập thành công.";
		//		return RedirectToAction(nameof(Index));
		//	}
		//	TempData["Error"] = "Đã có lỗi xảy ra";
		//	return View(inventoryvoucher);
		//}

		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			await _inventoryvoucherService.DeleteInventoryVoucherAsync(id);

			return Ok();
		}
	}
}
