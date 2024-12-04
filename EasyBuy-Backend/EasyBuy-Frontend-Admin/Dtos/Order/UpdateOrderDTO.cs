using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using EasyBuy_Backend.Models.Enums;

namespace EasyBuy_Frontend_Admin.Dtos.Order
{
    public class UpdateOrderDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Trạng thái đơn hàng là bắt buộc.")]
        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }
    }
}
