using EasyBuy_Frontend_Admin.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace EasyBuy_Frontend_Admin.Dtos.Auth
{
	public class SignUpDTO
	{
		[Required(ErrorMessage = "Tên là bắt buộc.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Email là bắt buộc.")]
		[EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
		[Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
		public string? Phone { get; set; }

		[Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
		[StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có độ dài từ 6 đến 100 ký tự.")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Địa chỉ là bắt buộc.")]
		[StringLength(200, MinimumLength = 6, ErrorMessage = "Địa chỉ phải có độ dài từ 6 đến 200 ký tự.")]
		public string Address { get; set; }

		public UserStatus Status { get; set; } = UserStatus.ENABLE;

		public UserRole Role { get; set; } = UserRole.ADMIN;
	}
}
