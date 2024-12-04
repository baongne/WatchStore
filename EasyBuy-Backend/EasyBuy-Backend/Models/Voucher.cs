using System;
using EasyBuy_Backend.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EasyBuy_Backend.Models
{
    public class Voucher
    {
        public int Id { get; set; } 

        public string VoucherName { get; set; }

        public int Discount { get; set; }

        public string Unit { get; set; } 

        public DateTime DateFrom { get; set; } 

        public DateTime DateTo { get; set; }

        public VoucherStatus Status { get; set; }

        [JsonIgnore]
        public virtual ICollection<Order>? Orders { get; set; }
    }
}
