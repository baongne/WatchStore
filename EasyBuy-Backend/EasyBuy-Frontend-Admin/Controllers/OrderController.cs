using EasyBuy_Frontend_Admin.Models;
using EasyBuy_Frontend_Admin.Services.OrderSvc;
using EasyBuy_Frontend_Admin.Services.VoucherSvc;
using EasyBuy_Frontend_Admin.Services.UserSvc;
//using EasyBuy_Frontend_Admin.Services.PaymentSvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EasyBuy_Frontend_Admin.Dtos.Order;
using System.Diagnostics;

namespace EasyBuy_Frontend_Admin.Controllers
{
	public class OrderController : Controller
	{
		private readonly IOrderService _orderService;
		private readonly IUserService _userService;
		private readonly IVoucherService _voucherService;
		//private readonly IPaymentService _paymentService;


		public OrderController(
			IOrderService orderService,
			IUserService userService,
			IVoucherService voucherService
			//,IPaymentService paymentService
		)
		{
			_orderService = orderService;
			_userService = userService;
			_voucherService = voucherService;
			//_paymentService = paymentService;
		}

		public async Task<IActionResult> Index()
		{
			List<OrderViewModel> orderies = await _orderService.GetOrderiesAsync();

			return View(orderies);
		}


        public async Task<IActionResult> Show(int id)
        {
            OrderViewModel order = await _orderService.GetOrderByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateOrderDTO updateOrderDTO)
        {
            if (await _orderService.UpdateOrderAsync(updateOrderDTO))
            {
				return Json(new { success = true, message = "Chỉnh sửa thành công." });
            }
            return Json(new { success = false, message = "Có lỗi xảy ra khi chỉnh sửa." });
        }
    }
}
