using EasyBuy_Frontend_Admin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using EasyBuy_Frontend_Admin.Models.Enums;
using EasyBuy_Backend.Models.Enums;


namespace EasyBuy_Frontend_Admin.Helpers
{
	public static class Helper
	{
		//Lấy ra tên display name của enum khi show
		public static string GetDisplayName(this Enum enumValue)
		{
			return enumValue.GetType()
							.GetMember(enumValue.ToString())
							.First()
							.GetCustomAttribute<DisplayAttribute>()?.Name ?? enumValue.ToString();
		}

		public static string GetBadge(this Enum enumValue)
		{
			switch (enumValue)
			{
				// Kiểm tra từng loại enum
				case CategoryStatus categoryStatus:
					switch (categoryStatus)
					{
						case CategoryStatus.ENABLE:
							return "success";
						case CategoryStatus.DISABLED:
							return "danger";
					}
					break;

				case ProductStatus productStatus:
					switch (productStatus)
					{
						case ProductStatus.ENABLE:
							return "success";
						case ProductStatus.OUT_OF_STOCK:
							return "warning";
						case ProductStatus.DISCONTINUED:
							return "danger";
					}
					break;
				case UserStatus userStatus:
					switch (userStatus)
					{
						case UserStatus.ENABLE:
							return "success";
						case UserStatus.DISABLED:
							return "danger";
					}
					break;

				case UserRole userRole:
					switch (userRole)
					{
						case UserRole.USER:
							return "info";
						case UserRole.ADMIN:
							return "primary";
					}
					break;

				case VoucherStatus voucherStatus:
					switch (voucherStatus)
					{
						case VoucherStatus.ENABLE:
							return "success";
						case VoucherStatus.DISABLED:
							return "danger";
					}
					break;

				case SupplierStatus supplierStatus:
					switch (supplierStatus)
					{
						case SupplierStatus.ENABLE:
							return "success";
						case SupplierStatus.DISABLED:
							return "danger";
					}
					break;

				case OrderStatus orderStatus:
					switch (orderStatus)
					{
						case OrderStatus.SUCCESS:
							return "success";
						case OrderStatus.CANCELED:
							return "danger";
						case OrderStatus.PENDING:
							return "warning";
					}
					break;

				default:
					return "secondary";
			}

			return "secondary"; 
		}
    }
}
