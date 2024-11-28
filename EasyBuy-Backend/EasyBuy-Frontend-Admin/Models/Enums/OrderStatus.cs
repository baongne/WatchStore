using System.ComponentModel.DataAnnotations;

namespace EasyBuy_Backend.Models.Enums
{
	public enum OrderStatus
	{
		[Display(Name = "Thành công")]
		SUCCESS = 0,

		[Display(Name = "Đã hủy")]
		CANCELED = 1,

		[Display(Name = "Đang xét duyệt")]
		PENDING = 2
	}
}
