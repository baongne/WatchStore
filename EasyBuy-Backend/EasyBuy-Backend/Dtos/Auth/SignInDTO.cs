using System.ComponentModel.DataAnnotations;

namespace EasyBuy_Backend.Dtos.Auth
{
    public class SignInDTO
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
