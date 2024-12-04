using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EasyBuy_Backend.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        [StringLength(50)]
        public string PaymentName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Order>? Orders { get; set; }
    }
}
