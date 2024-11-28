using EasyBuy_Frontend_Admin.Models;
using System.Diagnostics;
using System.Text.Json;

namespace EasyBuy_Frontend_Admin.Services.CategorySvc
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;
        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7084");
        }

        public async Task<List<CategoryViewModel>> GetCategoriesAsync()
        {
            List<CategoryViewModel> categories = new List<CategoryViewModel>();

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("/api/Category");
                string data = await response.Content.ReadAsStringAsync();
                categories = JsonSerializer.Deserialize<List<CategoryViewModel>>(data);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred: " + ex.Message);
            }

            return categories;
        }

        public async Task<bool> AddCategoryAsync(CategoryViewModel category)
        {
            try
            {
				Debug.WriteLine($"Status: {category.Status}");
				var response = await _httpClient.PostAsJsonAsync("/api/Category", category);

				if (response.IsSuccessStatusCode)
				{
					return true;
				}
				return false;
			}
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred while adding category: " + ex.Message);
                return false;
            }
        }

        public async Task<CategoryViewModel> GetCategoryByIdAsync(int id)
        {
            CategoryViewModel category = null;

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"/api/Category/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    category = JsonSerializer.Deserialize<CategoryViewModel>(data);
                }
                else
                {
                    Debug.WriteLine("Failed to retrieve category. Status code: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred while getting category: " + ex.Message);
            }

            return category;
        }

        public async Task<bool> UpdateCategoryAsync(CategoryViewModel category)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"/api/Category/{category.Id}", category);

				if (response.IsSuccessStatusCode)
				{
					return true;
				}
				return false;
			}
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred while updating category: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/Category/{id}");

				if (response.IsSuccessStatusCode)
				{
					return true;
				}
				return false;
			}
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred while deleting category: " + ex.Message);
                return false;
            }
        }

		public async Task<List<CategoryViewModel>> GetCategoriesActive()
		{
			List<CategoryViewModel> categories = new List<CategoryViewModel>();

			try
			{
				HttpResponseMessage response = await _httpClient.GetAsync("/api/Category/active");
				string data = await response.Content.ReadAsStringAsync();
				categories = JsonSerializer.Deserialize<List<CategoryViewModel>>(data);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("An error occurred: " + ex.Message);
			}

			return categories;
		}
	}
}
