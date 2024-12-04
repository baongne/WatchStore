using EasyBuy_Backend.Models;
using EasyBuy_Backend.Repositories.VoucherRepo;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;

namespace EasyBuy_Backend.Controllers
{
	//localhost:xxxx/api/Voucher
	[Route("api/[controller]")]
	[ApiController]
	public class VoucherController : ControllerBase
	{
		private readonly IVoucherRepository _voucherRepository;

		public VoucherController(IVoucherRepository voucherRepository)
		{
			_voucherRepository = voucherRepository;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var vouchers = _voucherRepository.GetAll();
			return Ok(vouchers);
		}

		[HttpGet("{id}")]

		public IActionResult GetById(int id)
		{
			var voucher = _voucherRepository.GetById(id);

			return Ok(voucher);
		}

	
		[HttpPost]
		public IActionResult Create([FromBody] Voucher voucher)
		{
			if (_voucherRepository.Create(voucher))
			{
				return Ok();
			}
			return BadRequest();
		}

		[HttpPut("{id}")]
		public IActionResult Update([FromBody] Voucher voucher)
		{
			if (_voucherRepository.Update(voucher))
			{
				return Ok();
			}
			return BadRequest();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var voucher = _voucherRepository.GetById(id);

			if (_voucherRepository.Delete(voucher))
			{
				return Ok();
			}
			return BadRequest();
		}
		
	}
}
