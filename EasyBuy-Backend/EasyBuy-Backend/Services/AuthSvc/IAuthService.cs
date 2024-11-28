using EasyBuy_Backend.Models;

namespace EasyBuy_Backend.Services.AuthSvc
{
    public interface IAuthService
    {
        Task<string> Register(User user);
		string Login(string email, string password);
	}
}
