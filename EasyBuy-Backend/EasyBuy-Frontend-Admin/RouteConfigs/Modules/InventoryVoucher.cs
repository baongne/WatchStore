namespace EasyBuy_Frontend_Admin.RouteConfigs.Modules
{
    public class InventoryVoucher
    {
        public static void RegisterRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                name: "inventory_voucher",
                pattern: "{controller=InventoryVoucher}/{action=Index}/{id?}");
        }
    }
}
