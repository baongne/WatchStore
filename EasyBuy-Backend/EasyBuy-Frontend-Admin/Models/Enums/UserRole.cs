using System.ComponentModel.DataAnnotations;

namespace EasyBuy_Frontend_Admin.Models.Enums
{
	public enum UserRole
	{
		[Display(Name = "Người dùng")]
		USER = 0,

		[Display(Name = "Quản trị viên")]
		ADMIN = 1
	}
}
