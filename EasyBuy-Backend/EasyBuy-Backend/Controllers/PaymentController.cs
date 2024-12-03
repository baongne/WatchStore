using EasyBuy_Backend.Models;
using EasyBuy_Backend.Models.Vnpay;
using EasyBuy_Backend.Repositories.PaymentRepo;
using EasyBuy_Backend.Services.Vnpay;
using Microsoft.AspNetCore.Mvc;

namespace EasyBuy_Backend.Controllers
{
	//localhost:xxxx/api/Supplier
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentController : ControllerBase
	{
		private readonly IPaymentRepository _paymentRepository;

        private readonly IVnPayService _vnPayService;

        public PaymentController(
			IPaymentRepository paymentRepository,
            IVnPayService vnPayService

        ) {
			_paymentRepository = paymentRepository;
            _vnPayService = vnPayService;
        }

		[HttpGet]
		public IActionResult GetAll()
		{
			var payments = _paymentRepository.GetAll();

			return Ok(payments);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var payment = _paymentRepository.GetById(id);

			return Ok(payment);
		}

		[HttpPost]
		public IActionResult Create([FromBody] Payment payment)
		{
			if (_paymentRepository.Create(payment))
			{
				return Ok();
			}
			return BadRequest();
		}

		[HttpPut("{id}")]
		public IActionResult Update([FromBody] Payment payment)
		{
			if (_paymentRepository.Update(payment))
			{
				return Ok();
			}
			return BadRequest();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var payment = _paymentRepository.GetById(id);

			if (_paymentRepository.Delete(payment))
			{
				return Ok();
			}
			return BadRequest();
		}

        [HttpPost("create-url")]
        public IActionResult CreatePaymentUrlVnpay(PaymentInformationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Redirect(url);
        }

    }
}
