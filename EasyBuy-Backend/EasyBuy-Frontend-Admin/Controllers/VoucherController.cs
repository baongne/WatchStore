using EasyBuy_Frontend_Admin.Models;
using EasyBuy_Frontend_Admin.Services.VoucherSvc;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EasyBuy_Frontend_Admin.Controllers
{
	public class VoucherController : Controller
	{
		private readonly IVoucherService _voucherService;

		public VoucherController(IVoucherService voucherService)
		{
			_voucherService = voucherService;
		}

		public async Task<IActionResult> Index()
		{
			List<VoucherViewModel> vouchers = await _voucherService.GetVouchersAsync();

			return View(vouchers);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(VoucherViewModel voucher)
		{
			if (await _voucherService.AddVoucherAsync(voucher))
			{
				TempData["Success"] = "Thêm mới phiếu giảm giá thành công.";
				return RedirectToAction(nameof(Index));
			}
			TempData["Error"] = "Đã có lỗi xảy ra";
			return View(voucher);
		}

		public async Task<IActionResult> Edit(int id)
		{
			VoucherViewModel voucher = await _voucherService.GetVoucherByIdAsync(id);

			return View(voucher);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(VoucherViewModel voucher)
		{
			if (await _voucherService.UpdateVoucherAsync(voucher))
			{
				TempData["Success"] = "Chỉnh sửa phiếu giảm giá thành công.";
				return RedirectToAction(nameof(Index));
			}
			TempData["Error"] = "Đã có lỗi xảy ra";
			return View(voucher);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			await _voucherService.DeleteVoucherAsync(id);

			return Ok();
		}
	}
}
