using EasyBuy_Backend.Models;
using EasyBuy_Backend.Repositories.SupplierRepo;
using Microsoft.AspNetCore.Mvc;

namespace EasyBuy_Backend.Controllers
{
	//localhost:xxxx/api/Supplier
	[Route("api/[controller]")]
	[ApiController]
	public class SupplierController : ControllerBase
	{
		private readonly ISupplierRepository _supplierRepository;
		public SupplierController(ISupplierRepository supplierRepository)
		{
			_supplierRepository = supplierRepository;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var suppliers = _supplierRepository.GetAll();

			return Ok(suppliers);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var supplier = _supplierRepository.GetById(id);

			return Ok(supplier);
		}

		[HttpPost]
		public IActionResult Create([FromBody] Supplier supplier)
		{
			if (_supplierRepository.Create(supplier))
			{
				return Ok();
			}
			return BadRequest();
		}

		[HttpPut("{id}")]
		public IActionResult Update([FromBody] Supplier supplier)
		{
			if (_supplierRepository.Update(supplier))
			{
				return Ok();
			}
			return BadRequest();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var supplier = _supplierRepository.GetById(id);

			if (_supplierRepository.Delete(supplier))
			{
				return Ok();
			}
			return BadRequest();
		}

		[HttpGet("active")]
		public IActionResult GetActiveSuppliers()
		{
			var activeSuppliers = _supplierRepository.GetActiveSuppliers();

			return Ok(activeSuppliers);
		}
	}
}
