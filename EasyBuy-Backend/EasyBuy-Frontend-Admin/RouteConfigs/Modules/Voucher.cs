namespace EasyBuy_Frontend_Admin.RouteConfigs.Modules
{
	public class Voucher
	{
		public static void RegisterRoutes(IEndpointRouteBuilder endpoints)
		{
			endpoints.MapControllerRoute(
				name: "voucher",
				pattern: "{controller=Voucher}/{action=Index}/{id?}");
		}
	}
}
