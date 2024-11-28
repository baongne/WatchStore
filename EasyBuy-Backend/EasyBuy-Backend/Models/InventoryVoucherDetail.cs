using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EasyBuy_Backend.Models
{
    public class InventoryVoucherDetail
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        public int Quantity { get; set; } 

        [Required]
        public double ReceivingUnitPrice { get; set; }

        public int InventoryVoucherId { get; set; }

        [ForeignKey("InventoryVoucherId")]
        [JsonIgnore]
        public InventoryVoucher? InventoryVoucher { get; set; } 

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
    }
}
