namespace EasyBuy_Frontend_Admin.RouteConfigs.Modules
{
    public class Category
    {
        public static void RegisterRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                name: "category",
                pattern: "{controller=Category}/{action=Index}/{id?}");
        }
    }
}
