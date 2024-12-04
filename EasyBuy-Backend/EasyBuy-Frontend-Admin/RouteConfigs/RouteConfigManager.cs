using EasyBuy_Frontend_Admin.RouteConfigs.Modules;

namespace EasyBuy_Frontend_Admin.RouteConfigs
{
     public class RouteConfigManager
    {
        public static void RegisterAllRoutes(IEndpointRouteBuilder endpoints)
        {
			// Gọi tất cả các route config từ các lớp trong folder Modules
			Auth.RegisterRoutes(endpoints);
			Dashboard.RegisterRoutes(endpoints);
            User.RegisterRoutes(endpoints);
            Category.RegisterRoutes(endpoints);
            Supplier.RegisterRoutes(endpoints);
            Voucher.RegisterRoutes(endpoints);
            Product.RegisterRoutes(endpoints);
            InventoryVoucher.RegisterRoutes(endpoints);
        }
    }
}
