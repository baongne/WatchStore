using EasyBuy_Backend.Models;
using EasyBuy_Backend.Repositories.OrderlineRepo;
using Microsoft.AspNetCore.Mvc;

namespace EasyBuy_Backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderlineController : ControllerBase
	{
		private readonly IOrderlineRepository _orderLineRepository;

		public OrderlineController(IOrderlineRepository orderLineRepository)
		{
			_orderLineRepository = orderLineRepository;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var orderLines = _orderLineRepository.GetAll();
			return Ok(orderLines);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var orderLine = _orderLineRepository.GetById(id);
			return Ok(orderLine);
		}

		[HttpPost]
		public IActionResult Create([FromBody] OrderLine orderLine)
		{
			if (_orderLineRepository.Create(orderLine))
			{
				return Ok();
			}
			return BadRequest();
		}

		[HttpPut("{id}")]
		public IActionResult Update([FromBody] OrderLine orderLine)
		{
			if (_orderLineRepository.Update(orderLine))
			{
				return Ok();
			}
			return BadRequest();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var orderLine = _orderLineRepository.GetById(id);
			if (_orderLineRepository.Delete(orderLine))
			{
				return Ok();
			}
			return BadRequest();
		}

	}
}
