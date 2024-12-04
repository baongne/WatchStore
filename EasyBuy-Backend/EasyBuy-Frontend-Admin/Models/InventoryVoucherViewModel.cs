using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EasyBuy_Frontend_Admin.Models
{
	public class InventoryVoucherViewModel
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[Required(ErrorMessage = "Mã nhà cung cấp là bắt buộc.")]
		[JsonPropertyName("supplierId")]
		public int SupplierId { get; set; }

		[Required(ErrorMessage = "Tổng cộng là bắt buộc.")]
		[JsonPropertyName("total")]
		public decimal Total { get; set; } 

		[Required(ErrorMessage = "Ngày nhập là bắt buộc.")]
		[JsonPropertyName("date")]
		public DateTime Date { get; set; }

		[JsonPropertyName("supplier")]
		public SupplierViewModel? Supplier { get; set; }

		[JsonPropertyName("inventoryVoucherDetails")]
		public List<InventoryVoucherDetailViewModel>? InventoryVoucherDetailViewModels { get; set; }
	}
}
