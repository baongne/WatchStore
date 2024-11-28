using EasyBuy_Backend.Models;

namespace EasyBuy_Backend.Repositories.UserRepo
{
    public interface IUserRepository : IRepository<User>
    {
		User GetByEmail(string email);
	}
}
