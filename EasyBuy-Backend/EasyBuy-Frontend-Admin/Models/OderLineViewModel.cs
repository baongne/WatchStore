using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EasyBuy_Frontend_Admin.Models
{
    public class OrderLineViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Số lượng sản phẩm là bắt buộc.")]
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Đơn giá là bắt buộc.")]
        [JsonPropertyName("unitPrice")]
        public double UnitPrice { get; set; }

        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }

        // Không cần khóa ngoại ở ViewModel, chỉ cần tham chiếu
        [JsonPropertyName("order")]
        public OrderViewModel? Order { get; set; }

        [JsonPropertyName("productId")]
        public int ProductId { get; set; }

        // Không cần khóa ngoại ở ViewModel, chỉ cần tham chiếu
        [JsonPropertyName("product")]
        public ProductViewModel? Product { get; set; }

        // Thêm thuộc tính tính toán tổng tiền (không cần lưu trong cơ sở dữ liệu)
        [JsonPropertyName("totalPrice")]
        public double TotalPrice => Quantity * UnitPrice;
    }
}
