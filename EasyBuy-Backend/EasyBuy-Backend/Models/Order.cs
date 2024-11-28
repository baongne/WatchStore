using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using EasyBuy_Backend.Models.Enums;

namespace EasyBuy_Backend.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public int ShippingFee { get; set; } 

        [Required]
        public int OrderDiscount { get; set; } 

        [Required]
        public double OrderTotal { get; set; } 

        [Required]
        [StringLength(150)]
        public string Address { get; set; } 

        [Required]
        public OrderStatus Status { get; set; }

        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        public int? PaymentId { get; set; }

        [ForeignKey("PaymentId")]
        public Payment? Payment { get; set; }

        public int? VoucherId { get; set; }

        [ForeignKey("VoucherId")]
        public Voucher? Voucher { get; set; }

        public virtual ICollection<OrderLine>? OrderLines { get; set; }
    }
}
