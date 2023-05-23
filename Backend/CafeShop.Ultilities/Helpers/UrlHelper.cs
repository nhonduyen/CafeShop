using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace CafeShop.Ultilities.Helpers {
    public static class UrlHelper {

        public static string? GetCurrentUrl(HttpRequest? httpRequest, IConfiguration configuration, string urlConfigKey) {
            if (httpRequest == null) return null;

            var imageUrl = configuration[urlConfigKey];
            if (!string.IsNullOrWhiteSpace(imageUrl))
                return imageUrl;

            return $"{httpRequest.Scheme}://{httpRequest.Host.Value}/";
        }
    }
}