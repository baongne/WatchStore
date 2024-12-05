using EasyBuy_Backend.Repositories.CartRepo;
using EasyBuy_Backend.Repositories.InventoryVoucherRepo;
using EasyBuy_Backend.Repositories.SupplierRepo;
using EasyBuy_Backend.Repositories.VoucherRepo;
using EasyBuy_Backend.Services.AuthSvc;
using EasyBuy_Backend.Services.Vnpay;

namespace EasyBuy_Backend.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Register services here
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IInventoryVoucherRepository, InventoryVoucherRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
			return services;
        }
    }
}
