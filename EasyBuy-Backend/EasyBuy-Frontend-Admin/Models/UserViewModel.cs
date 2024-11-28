using EasyBuy_Frontend_Admin.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EasyBuy_Frontend_Admin.Models
{
    public class UserViewModel
    {
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[Required(ErrorMessage = "Tên là bắt buộc.")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

		[Required(ErrorMessage = "Email là bắt buộc.")]
		[EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
        [JsonPropertyName("email")]
        public string Email { get; set; }

		[Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
		[Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        [JsonPropertyName("phone")]
        public string? PhoneNumber { get; set; }

		[Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
		[StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có độ dài từ 6 đến 100 ký tự.")]
        [JsonPropertyName("password")]
        public string Password { get; set; }

		[Required(ErrorMessage = "Địa chỉ là bắt buộc.")]
		[StringLength(200, MinimumLength = 6, ErrorMessage = "Địa chỉ phải có độ dài từ 6 đến 200 ký tự.")]
		[JsonPropertyName("address")]
		public string Address { get; set; }

        [Required(ErrorMessage = "Trạng thái là bắt buộc.")]
        [JsonPropertyName("status")]
        public UserStatus Status { get; set; }

        [Required(ErrorMessage = "Vai trò là bắt buộc.")]
        [JsonPropertyName("role")]
        public UserRole Role { get; set; }
    }
}
