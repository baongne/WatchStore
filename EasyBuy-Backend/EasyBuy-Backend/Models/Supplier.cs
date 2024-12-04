using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using EasyBuy_Backend.Models.Enums;


namespace EasyBuy_Backend.Models
{
    public class Supplier
    {
		public int Id { get; set; } 

		public string Name { get; set; }

		public string NumberPhone { get; set; }

		public string Address { get; set; } 

		public string Email { get; set; } 

		public SupplierStatus Status { get; set; }

        [JsonIgnore]
        public virtual ICollection<InventoryVoucher>? InventoryVouchers { get; set; } 
	}
}
