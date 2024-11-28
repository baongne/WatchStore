using EasyBuy_Frontend_Admin.Models;

namespace EasyBuy_Frontend_Admin.Services.UserSvc
{
	public interface IUserService
	{
		Task<List<UserViewModel>> GetUsersAsync();

		Task<bool> AddUserAsync(UserViewModel user);

		Task<UserViewModel> GetUserByIdAsync(int id);

        Task<bool> UpdateUserAsync(UserViewModel user);

        Task<bool> DeleteUserAsync(int id);

		Task<UserViewModel> GetUserByEmailAsync(string email);

    }
}
