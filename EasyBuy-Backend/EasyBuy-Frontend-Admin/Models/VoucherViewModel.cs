using EasyBuy_Frontend_Admin.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EasyBuy_Frontend_Admin.Models
{
	public class VoucherViewModel : IValidatableObject
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[Required(ErrorMessage = "Tên là bắt buộc.")]
		[JsonPropertyName("voucherName")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Giá trị là bắt buộc.")]
		[Range(1, int.MaxValue, ErrorMessage = "Giá trị phải lớn hơn 0.")]
		[RegularExpression(@"^\d+$", ErrorMessage = "Giá trị không được chứa kí tự.")]
		[JsonPropertyName("discount")]
		public int Discount { get; set; }

		[Required(ErrorMessage = "Đơn vị là bắt buộc.")]
		[StringLength(200, MinimumLength = 1, ErrorMessage = "Đơn vị phải có độ dài từ 1 đến 200 ký tự.")]
		[JsonPropertyName("unit")]
		public string Unit { get; set; }

		[Required(ErrorMessage = "Ngày bắt đầu là bắt buộc.")]
		[JsonPropertyName("dateFrom")]
		public DateTime DateFrom { get; set; }

		[Required(ErrorMessage = "Ngày kết thúc là bắt buộc.")]
		[JsonPropertyName("dateTo")]
		public DateTime DateTo { get; set; }


		[Required(ErrorMessage = "Trạng thái là bắt buộc.")]
		[JsonPropertyName("status")]
		public VoucherStatus Status { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (DateFrom >= DateTo)
			{
				yield return new ValidationResult(
					"Ngày bắt đầu phải nhỏ hơn ngày kết thúc.",
					new[] { nameof(DateFrom), nameof(DateTo) }
				);
			}
		}

	}
}
