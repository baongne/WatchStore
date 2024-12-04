using EasyBuy_Backend.Models;
using EasyBuy_Backend.Repositories.CategoryRepo;
using EasyBuy_Backend.Repositories.UserRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyBuy_Backend.Controllers
{
    //localhost:xxxx/api/Category
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _categoryRepository.GetAll();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _categoryRepository.GetById(id);

            return Ok(category);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Category category)
        {
            if (_categoryRepository.Create(category))
            {
				return Ok();
			}
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] Category category, int id)
        {
            category.Id = id;
            if(_categoryRepository.Update(category))
			{
				return Ok();
			}
			return BadRequest();
		}

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _categoryRepository.GetById(id);

            if(_categoryRepository.Delete(category))
			{
				return Ok();
			}
			return BadRequest();
		}

		[HttpGet("active")]
		public IActionResult GetActiveCategories()
		{
			var activeCategories = _categoryRepository.GetActiveCategories();

			return Ok(activeCategories);
		}
	}
}
