using EasyBuy_Backend.Models;
using EasyBuy_Backend.Repositories.UserRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EasyBuy_Backend.Controllers
{
    //localhost:xxxx/api/User
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userRepository.GetAll();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            //var user = _userRepository.GetUserIdAsString(id);

            var user = _userRepository.GetById(id);
           
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            if (_userRepository.Create(user))
            {
				return Ok();
			}
			return BadRequest();
		}

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] User user, int id)
        {
            user.Id = id;
            if (_userRepository.Update(user))
            {
				return Ok();
			}
			return BadRequest();
		}

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _userRepository.GetById(id);

            if (_userRepository.Delete(user))
            {
				return Ok();
			}
			return BadRequest();
		}

        [HttpGet("email/{email}")]
        public IActionResult GetByEmail(string email)
        {
            var user = _userRepository.GetByEmail(email);

            if (user != null)
            {
                return Ok(user);
            }

            return NotFound(new { Message = "User not found" });
        }
    }
}
