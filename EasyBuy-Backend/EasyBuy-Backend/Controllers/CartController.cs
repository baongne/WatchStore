using EasyBuy_Backend.Models;
using EasyBuy_Backend.Repositories.CartRepo;
using EasyBuy_Backend.Repositories.CategoryRepo;
using EasyBuy_Backend.Repositories.ProductRepo;
using Microsoft.AspNetCore.Mvc;

namespace EasyBuy_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var carts = _cartRepository.GetAll();

            return Ok(carts);
        }
		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var cart = _cartRepository.GetById(id);
			return Ok(cart);
		}

		[HttpPost]
		public IActionResult Create([FromBody] Cart cart)
		{
			if (_cartRepository.Create(cart))
			{
				return Ok();
			}
			return BadRequest();
		}

		[HttpPut("{id}")]
		public IActionResult Update([FromBody] Cart cart, int id)
		{
			cart.Id = id;
			if (_cartRepository.Update(cart))
			{
				return Ok();
			}
			return BadRequest();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var cart = _cartRepository.GetById(id);
			if (_cartRepository.Delete(cart))
			{
				return Ok();
			}
			return BadRequest();
		}
		[HttpDelete("DeleteCart/{userId}")]
		public async Task<IActionResult> DeleteCart(int userId)
		{
			var result = await _cartRepository.DeleteCartByUserIdAsync(userId);
			return Ok(result); // Trả về kết quả boolean trực tiếp
		}
	}

		
		
    }

