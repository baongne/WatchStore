namespace EasyBuy_Frontend_Admin.RouteConfigs.Modules
{
    public class Supplier
    {
        public static void RegisterRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                name: "supplier",
                pattern: "{controller=Supplier}/{action=Index}/{id?}");
        }
    }
}
