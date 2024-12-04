using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EasyBuy_Frontend_Admin.Models

{
	public class DashboardViewModel
	{
			public int TotalProducts { get; set; }      
			public int TotalCategories { get; set; }   
			public int TotalOrders { get; set; }       
			public int TotalCustomers { get; set; }   
			public int TotalInventoryVouchers { get; set; }     
		}
	}
