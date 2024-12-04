using EasyBuy_Frontend_Admin.Dtos.InventoryVoucherDetail;
using EasyBuy_Frontend_Admin.Models;
using System.Diagnostics;
using System.Text.Json;

namespace EasyBuy_Frontend_Admin.Services.InventoryVoucherSvc
{
	public class InventoryVoucherService : IInventoryVoucherService
	{
		private readonly HttpClient _httpClient;

		public InventoryVoucherService(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri("https://localhost:7084");
		}

		public async Task<List<InventoryVoucherViewModel>> GetInventoryVoucheriesAsync()
		{
			List<InventoryVoucherViewModel> inventoryvoucheries = new List<InventoryVoucherViewModel>();

			try
			{
				HttpResponseMessage response = await _httpClient.GetAsync("/api/InventoryVoucher");

				string data = await response.Content.ReadAsStringAsync();
				inventoryvoucheries = JsonSerializer.Deserialize<List<InventoryVoucherViewModel>>(data);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("An error occurred: " + ex.Message);
			}

			return inventoryvoucheries;
		}

        public bool AddInventoryVoucher(List<InventoryVoucherDetailDTO> details)
        {
            try
            {
                var firstDetail = details.FirstOrDefault();

                var inventoryvoucher = new InventoryVoucherViewModel
                {
                    SupplierId = firstDetail.SupplierId,
                    Total = details.Sum(d => d.ReceivingUnitPrice * d.Quantity),
                    Date = firstDetail.Date
                };

                var response = _httpClient.PostAsJsonAsync("/api/InventoryVoucher", inventoryvoucher).Result;
               
                var responseData = response.Content.ReadAsStringAsync().Result;

                var createdVoucher = JsonSerializer.Deserialize<InventoryVoucherViewModel>(responseData);
                    if (createdVoucher != null)
                    {
                        foreach (var detail in details)
                        {
                            var voucherDetail = new InventoryVoucherDetailViewModel
                            {
                                InventoryVoucherId = createdVoucher.Id,
                                ProductId = detail.ProductId,
                                Quantity = detail.Quantity,
                                ReceivingUnitPrice = detail.ReceivingUnitPrice
                            };

                            _httpClient.PostAsJsonAsync("/api/InventoryVoucherDetail", voucherDetail).Wait();
                        }
                        return true;
                    }
             
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred while adding inventoryvoucher: " + ex.Message);
                return false;
            }
        }


        public async Task<InventoryVoucherViewModel> GetInventoryVoucherByIdAsync(int id)
		{
			InventoryVoucherViewModel inventoryvoucher = null;

			try
			{
				HttpResponseMessage response = await _httpClient.GetAsync($"/api/InventoryVoucher/{id}");

				string data = await response.Content.ReadAsStringAsync();
				inventoryvoucher = JsonSerializer.Deserialize<InventoryVoucherViewModel>(data);

			}
			catch (Exception ex)
			{
				Debug.WriteLine("An error occurred while getting inventory voucher: " + ex.Message);
			}

			return inventoryvoucher;
		}

		public async Task<bool> UpdateInventoryVoucherAsync(InventoryVoucherViewModel inventoryvoucher)
		{
			try
			{
				var response = await _httpClient.PutAsJsonAsync($"/api/InventoryVoucher/{inventoryvoucher.Id}", inventoryvoucher);

				if (response.IsSuccessStatusCode)
				{
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				Debug.WriteLine("An error occurred while updating inventory voucher: " + ex.Message);
				return false;
			}
		}

		public async Task<bool> DeleteInventoryVoucherAsync(int id)
		{
			try
			{
				var response = await _httpClient.DeleteAsync($"/api/InventoryVoucher/{id}");

				if (response.IsSuccessStatusCode)
				{
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				Debug.WriteLine("An error occurred while deleting inventory voucher: " + ex.Message);
				return false;
			}
		}		
	}
}
