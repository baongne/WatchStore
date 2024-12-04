using System.ComponentModel.DataAnnotations;

namespace EasyBuy_Frontend_Admin.Models.Enums
{
	public enum ProductGender
	{
		[Display(Name = "Nam")]
		ENABLE = 0,

		[Display(Name = "Nữ")]
		DISABLED = 1
	}
}
