using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyBuy_Backend.Models
{
    public class InventoryVoucher
    {
		[Key]
		public int Id { get; set; }

		[Required]
		public DateTime Date { get; set; }

		[Required]
		public double Total { get; set; }

		[Required]
		public int? SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public Supplier? Supplier { get; set; }

        public virtual ICollection<InventoryVoucherDetail>? InventoryVoucherDetails { get; set; }
    }
}
