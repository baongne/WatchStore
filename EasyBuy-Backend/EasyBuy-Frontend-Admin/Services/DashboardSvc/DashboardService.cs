using EasyBuy_Frontend_Admin.Models;
using EasyBuy_Frontend_Admin.Services.DashboardSvc;
using System.Diagnostics;
using System.Text.Json;

namespace EasyBuy_Frontend_Admin.Services.DashboardSvc
{
	public class DashboardService : IDashboardService
	{
		private readonly HttpClient _httpClient;


		public DashboardService(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri("https://localhost:7084");
		}

		public async Task<DashboardViewModel> GetDashboardAsync()
		{
			DashboardViewModel dashboard = null;

			try
			{
				HttpResponseMessage response = await _httpClient.GetAsync("/api/Dashboard/statistics");

				if (response.IsSuccessStatusCode)
				{
					string data = await response.Content.ReadAsStringAsync();
					dashboard = JsonSerializer.Deserialize<DashboardViewModel>(data, new JsonSerializerOptions
					{
						PropertyNameCaseInsensitive = true // Không phân biệt chữ hoa/chữ thường
					});
				}
				else
				{
					Debug.WriteLine($"API call failed with status code: {response.StatusCode}");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("An error occurred: " + ex.Message);
			}

			return dashboard;
		}
	}
}
