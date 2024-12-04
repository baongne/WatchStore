using System.ComponentModel.DataAnnotations;

namespace EasyBuy_Frontend_Admin.Dtos.Auth
{
	public class SignInDTO
	{
		[Required(ErrorMessage = "Email là bắt buộc.")]
		[EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
		[StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có độ dài từ 6 đến 100 ký tự.")]
		public string Password { get; set; }
	}
}
