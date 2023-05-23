using CafeShop.Common;
using CafeShop.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CafeShop.Controllers {
    public class BaseController : ControllerBase {
        protected readonly IMediator mediator;
        protected readonly IConfiguration configuration;
        protected readonly IHttpContextAccessor httpContextAccessor;
        protected readonly HttpContext? httpContext;

        protected readonly Guid merchantId;
        protected readonly Guid userId;

        protected BaseController(IServiceProvider serviceProvider) {
            this.mediator = serviceProvider.GetRequiredService<IMediator>();
            this.configuration = serviceProvider.GetRequiredService<IConfiguration>();
            this.httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            this.httpContext = this.httpContextAccessor.HttpContext;

            var merchantIdValue = httpContext?.User?.FindFirst(o => o.Type == Constants.TokenMerchantId)?.Value ?? string.Empty;
            var userIdValue = httpContext?.User?.FindFirst(o => o.Type == Constants.TokenUserId)?.Value ?? string.Empty;

            Guid.TryParse(merchantIdValue, out merchantId);
            Guid.TryParse(userIdValue, out userId);
        }

        protected FileContentResult File(FileRes file) {
            return File(file.ByteArray, "application/octet-stream", file.FileName);
        }
    }
}