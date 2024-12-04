using EasyBuy_Frontend_Admin.Models;
using System.Diagnostics;
using System.Text.Json;

namespace EasyBuy_Frontend_Admin.Services.SupplierSvc
{
    public class SupplierService : ISupplierService
    {
        private readonly HttpClient _httpClient;

        public SupplierService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7084");
        }

        public async Task<List<SupplierViewModel>> GetSuppliersAsync()
        {
            List<SupplierViewModel> suppliers = new List<SupplierViewModel>();

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync("/api/Supplier");

                string data = await response.Content.ReadAsStringAsync();
                suppliers = JsonSerializer.Deserialize<List<SupplierViewModel>>(data);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred: " + ex.Message);
            }

            return suppliers;
        }

        public async Task<bool> AddSupplierAsync(SupplierViewModel supplier)
        {
            try
            {
                Debug.WriteLine($"Status: {supplier.Status}");
                var response = await _httpClient.PostAsJsonAsync("/api/Supplier", supplier);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred while adding supplier: " + ex.Message);
                return false;
            }
        }

        public async Task<SupplierViewModel> GetSupplierByIdAsync(int id)
        {
            SupplierViewModel supplier = null;

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"/api/Supplier/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    supplier = JsonSerializer.Deserialize<SupplierViewModel>(data);
                }
                else
                {
                    Debug.WriteLine("Failed to retrieve supplier. Status code: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred while getting supplier: " + ex.Message);
            }

            return supplier;
        }

        public async Task<bool> UpdateSupplierAsync(SupplierViewModel supplier)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"/api/Supplier/{supplier.Id}", supplier);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred while updating supplier: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteSupplierAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/Supplier/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred while deleting supplier: " + ex.Message);
                return false;
            }
        }

		public async Task<List<SupplierViewModel>> GetSuppliersActive()
		{
			List<SupplierViewModel> suppliers = new List<SupplierViewModel>();

			try
			{
				HttpResponseMessage response = await _httpClient.GetAsync("/api/Supplier/active");

				string data = await response.Content.ReadAsStringAsync();
				suppliers = JsonSerializer.Deserialize<List<SupplierViewModel>>(data);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("An error occurred: " + ex.Message);
			}

			return suppliers;
		}
	}
}
    
