using EasyBuy_Frontend_Admin.Dtos.Auth;
using EasyBuy_Frontend_Admin.Models;

namespace EasyBuy_Frontend_Admin.Services.AuthSvc
{
    public interface IAuthService
    {
        Task<bool> Register(SignUpDTO signUpDTO);
        Task<string?> Login(SignInDTO signInDTO);
    }
}
