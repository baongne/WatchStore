namespace EasyBuy_Frontend_Admin.RouteConfigs.Modules
{
    public class Dashboard
    {
        public static void RegisterRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                name: "dashboard",
                pattern: "{controller=Dashboard}/{action=Index}/{id?}");
        }
    }
}
