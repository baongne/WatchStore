namespace EasyBuy_Frontend_Admin.RouteConfigs.Modules
{
    public class User
    {
        public static void RegisterRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                name: "user",
                pattern: "{controller=User}/{action=Index}/{id?}");
        }
    }
}
