using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EasyBuy_Frontend_Admin.Models
{
	public class InventoryVoucherDetailViewModel
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("quantity")]
		public int Quantity { get; set; }

		[JsonPropertyName("receivingUnitPrice")]
		public decimal ReceivingUnitPrice { get; set; }

		[Required(ErrorMessage = "Mã phiếu nhập là bắt buộc.")]
		[JsonPropertyName("inventoryVoucherId")]
		public int InventoryVoucherId { get; set; }

		[JsonPropertyName("inventoryVoucher")]
		public InventoryVoucherViewModel? InventoryVoucher { get; set; }

		[Required(ErrorMessage = "Mã sản phẩm là bắt buộc.")]
		[JsonPropertyName("productId")]
		public int ProductId { get; set; }

		[JsonPropertyName("product")]
		public ProductViewModel? Product { get; set; }
	}
}
