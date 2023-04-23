using Microsoft.Extensions.DependencyInjection;

namespace CafeShop.Handlers {
    public static class DependencyInjection {

        public static void AddHandlers(this IServiceCollection services) {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(BaseHandler).Assembly));
        }
    }
}