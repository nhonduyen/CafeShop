using Microsoft.Extensions.DependencyInjection;

namespace CafeShop.CommonServices {
    public static class DependencyInjection {

        public static void AddCommonServices(this IServiceCollection services) {
            services.AddScoped<IImageService, ImageService>();
        }
    }
}