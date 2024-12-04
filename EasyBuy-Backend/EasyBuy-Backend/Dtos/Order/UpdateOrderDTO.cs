using EasyBuy_Backend.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EasyBuy_Backend.Dtos.Order
{
    public class UpdateOrderDTO
    {
        public int Id { get; set; }

        public OrderStatus Status { get; set; }
    }
}
