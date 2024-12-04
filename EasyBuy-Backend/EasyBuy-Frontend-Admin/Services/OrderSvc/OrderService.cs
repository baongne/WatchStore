using EasyBuy_Frontend_Admin.Dtos.Order;
using EasyBuy_Frontend_Admin.Models;
using System.Diagnostics;
using System.Text.Json;

namespace EasyBuy_Frontend_Admin.Services.OrderSvc
{
	public class OrderService : IOrderService
	{
		private readonly HttpClient _httpClient;

		public OrderService(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri("https://localhost:7084");
		}

		public async Task<List<OrderViewModel>> GetOrderiesAsync()
		{
			List<OrderViewModel> orderies = new List<OrderViewModel>();

			try
			{
				HttpResponseMessage response = await _httpClient.GetAsync("/api/Order");

				string data = await response.Content.ReadAsStringAsync();
				orderies = JsonSerializer.Deserialize<List<OrderViewModel>>(data);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("An error occurred: " + ex.Message);
			}

			return orderies;
		}

		public async Task<bool> AddOrderAsync(OrderViewModel order)
		{
			try
			{
				var response = await _httpClient.PostAsJsonAsync("/api/Order", order);

				if (response.IsSuccessStatusCode)
				{
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				Debug.WriteLine("An error occurred while adding order: " + ex.Message);
				return false;
			}
		}

		public async Task<OrderViewModel> GetOrderByIdAsync(int id)
		{
			OrderViewModel order = null;

			try
			{
				HttpResponseMessage response = await _httpClient.GetAsync($"/api/Order/{id}");

				string data = await response.Content.ReadAsStringAsync();
				order = JsonSerializer.Deserialize<OrderViewModel>(data);

			}
			catch (Exception ex)
			{
				Debug.WriteLine("An error occurred while getting inventory voucher: " + ex.Message);
			}

			return order;
		}

		public async Task<bool> UpdateOrderAsync(UpdateOrderDTO order)
		{
			try
			{
				var response = await _httpClient.PutAsJsonAsync($"/api/Order/{order.Id}", order);

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

		public async Task<bool> DeleteOrderAsync(int id)
		{
			try
			{
				var response = await _httpClient.DeleteAsync($"/api/Order/{id}");

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
