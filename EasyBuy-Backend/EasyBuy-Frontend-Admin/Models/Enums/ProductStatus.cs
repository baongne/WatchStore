using System.ComponentModel.DataAnnotations;

namespace EasyBuy_Frontend_Admin.Models.Enums
{
	public enum ProductStatus
	{
		[Display(Name = "Còn Hàng")]
		ENABLE = 1,

		[Display(Name = "Hết Hàng")]
		OUT_OF_STOCK = 2,

		[Display(Name = "Ngừng Kinh Doanh")]
		DISCONTINUED = 3
	}
}
