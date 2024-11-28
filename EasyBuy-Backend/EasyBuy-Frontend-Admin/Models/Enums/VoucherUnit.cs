using System.ComponentModel.DataAnnotations;

namespace EasyBuy_Frontend_Admin.Models.Enums { 

public enum VoucherUnit
	{
		[Display(Name = "%")]
		PER = 0,

		[Display(Name = "VNĐ")]
		VND = 1
	}
}
