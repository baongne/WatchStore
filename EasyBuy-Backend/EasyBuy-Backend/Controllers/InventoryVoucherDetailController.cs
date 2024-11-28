using EasyBuy_Backend.Models;
using EasyBuy_Backend.Repositories.InventoryVoucherDetailRepo;
using EasyBuy_Backend.Repositories.InventoryVoucherRepo;
using EasyBuy_Backend.Repositories.ProductRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EasyBuy_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryVoucherDetailController : ControllerBase
    {
        private readonly IInventoryVoucherDetailRepository _inventoryVoucherDetailRepository;
        private readonly IProductRepository _productRepository;

        public InventoryVoucherDetailController(
            IInventoryVoucherDetailRepository inventoryVoucherDetailRepository,
            IProductRepository productRepository
        ) {
            _inventoryVoucherDetailRepository = inventoryVoucherDetailRepository;
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var inventoryVouchers = _inventoryVoucherDetailRepository.GetAll();

            return Ok(inventoryVouchers);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var inventoryVoucher = _inventoryVoucherDetailRepository.GetById(id);

            return Ok(inventoryVoucher);
        }

        [HttpPost]
        public IActionResult Create([FromBody] InventoryVoucherDetail inventoryVoucher)
        {
            if (_inventoryVoucherDetailRepository.Create(inventoryVoucher))
            {
                var product = _productRepository.GetById(inventoryVoucher.ProductId);
                Debug.WriteLine("An error occurred while adding inventoryvoucher: " + inventoryVoucher.ProductId);
                if (product != null)
                {
                    product.StockQuantity += inventoryVoucher.Quantity;

                    _productRepository.Update(product);
                }
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] InventoryVoucherDetail inventoryVoucher, int id)
        {
            inventoryVoucher.Id = id;
            if (_inventoryVoucherDetailRepository.Update(inventoryVoucher))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var inventoryVoucher = _inventoryVoucherDetailRepository.GetById(id);

            if (_inventoryVoucherDetailRepository.Delete(inventoryVoucher))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
