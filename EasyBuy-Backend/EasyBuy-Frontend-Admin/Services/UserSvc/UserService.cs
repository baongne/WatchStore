using EasyBuy_Frontend_Admin.Models;
using System.Diagnostics;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EasyBuy_Frontend_Admin.Services.UserSvc
{
	public class UserService : IUserService
	{
		private readonly HttpClient _httpClient;

		public UserService(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri("https://localhost:7084");
		}

		public async Task<List<UserViewModel>> GetUsersAsync()
		{
			List<UserViewModel> users = new List<UserViewModel>();

			try
			{
				HttpResponseMessage response = await _httpClient.GetAsync("/api/User");
		
				string data = await response.Content.ReadAsStringAsync();
				Debug.WriteLine("An error occurred: " + data);
				users = JsonSerializer.Deserialize<List<UserViewModel>>(data);
          
            }
			catch (Exception ex)
			{
				Debug.WriteLine("An error occurred: " + ex.Message);
			}

			return users;
		}

		public async Task<bool> AddUserAsync(UserViewModel user)
		{
			try
			{
                var response = await _httpClient.PostAsJsonAsync("/api/User", user);

				if (response.IsSuccessStatusCode)
				{
					return true; 
				}
				return false;
			}
			catch (Exception ex)
			{
				Debug.WriteLine("An error occurred while adding user: " + ex.Message);
                return false;
            }
		}

		public async Task<UserViewModel> GetUserByIdAsync(int id)
		{
			UserViewModel user = null;

			try
			{
				HttpResponseMessage response = await _httpClient.GetAsync($"/api/User/{id}");

				if (response.IsSuccessStatusCode)
				{
					string data = await response.Content.ReadAsStringAsync();
					user = JsonSerializer.Deserialize<UserViewModel>(data);
				}
				else
				{
					Debug.WriteLine("Failed to retrieve user. Status code: " + response.StatusCode);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("An error occurred while getting user: " + ex.Message);
			}

			return user;
		}

		public async Task<bool> UpdateUserAsync(UserViewModel user)
		{
			try
			{
				var response = await _httpClient.PutAsJsonAsync($"/api/User/{user.Id}", user);

				if (response.IsSuccessStatusCode)
				{
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				Debug.WriteLine("An error occurred while updating user: " + ex.Message);
                return false;
            }
		}

		public async Task<bool> DeleteUserAsync(int id)
		{
			try
			{
				var response = await _httpClient.DeleteAsync($"/api/User/{id}");

				if (response.IsSuccessStatusCode)
				{
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				Debug.WriteLine("An error occurred while deleting user: " + ex.Message);
                return false;
            }
		}

        public async Task<UserViewModel> GetUserByEmailAsync(string email)
        {
            UserViewModel user = null;

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"/api/User/email/{email}");

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    user = JsonSerializer.Deserialize<UserViewModel>(data);
                }
                else
                {
                    Debug.WriteLine("Failed to retrieve user by email. Status code: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred while getting user by email: " + ex.Message);
            }

            return user;
        }

    }
}
