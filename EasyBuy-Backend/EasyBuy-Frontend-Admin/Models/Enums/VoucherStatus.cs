using System.ComponentModel.DataAnnotations;

namespace EasyBuy_Frontend_Admin.Models.Enums
{
	public enum VoucherStatus
	{
		[Display(Name = "Hoạt động")]
		ENABLE = 0,

		[Display(Name = "Không hoạt động")]
		DISABLED = 1
	}
}
