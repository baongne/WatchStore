using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using EasyBuy_Backend.Models.Enums;

namespace EasyBuy_Frontend_Admin.Models
{
    public class OrderViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ngày đặt hàng là bắt buộc.")]
        [JsonPropertyName("orderDate")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Phí vận chuyển là bắt buộc.")]
        [JsonPropertyName("shippingFee")]
        public int ShippingFee { get; set; }

        [Required(ErrorMessage = "Giảm giá đơn hàng là bắt buộc.")]
        [JsonPropertyName("orderDiscount")]
        public int OrderDiscount { get; set; }

        [Required(ErrorMessage = "Tổng số tiền đơn hàng là bắt buộc.")]
        [JsonPropertyName("orderTotal")]
        public double OrderTotal { get; set; }

        [Required(ErrorMessage = "Địa chỉ là bắt buộc.")]
        [StringLength(150, ErrorMessage = "Địa chỉ không được vượt quá 150 ký tự.")]
        [JsonPropertyName("address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Trạng thái đơn hàng là bắt buộc.")]
        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }

        [JsonPropertyName("userId")]
        public int? UserId { get; set; }

        [JsonPropertyName("paymentId")]
        public int? PaymentId { get; set; }

        [JsonPropertyName("voucherId")]
        public int? VoucherId { get; set; }

        // Thông tin người dùng liên kết
        [JsonPropertyName("user")]
        public UserViewModel? User { get; set; }

        // Thông tin voucher liên kết
        [JsonPropertyName("voucher")]
        public VoucherViewModel? Voucher { get; set; }

        // Bổ sung thêm danh sách chi tiết đơn hàng (OrderLines)
        [JsonPropertyName("orderLines")]
        public List<OrderLineViewModel>? OrderLines { get; set; }
    }
}
