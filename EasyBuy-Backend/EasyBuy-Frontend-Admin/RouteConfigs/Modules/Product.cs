namespace EasyBuy_Frontend_Admin.RouteConfigs.Modules
{
	public class Product
	{
		public static void RegisterRoutes(IEndpointRouteBuilder endpoints)
		{
			endpoints.MapControllerRoute(
				name: "product",
				pattern: "{controller=Product}/{action=Index}/{id?}");
		}
	}
}
