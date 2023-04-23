using CafeShop.Common.Lock;
using CafeShop.Database;
using CafeShop.Ultilities.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CafeShop.Handlers {
    public abstract class BaseHandler {
        protected readonly IMediator mediator;
        protected readonly CafeShopContext db;
        protected readonly IConfiguration configuration;
        protected readonly IHttpContextAccessor httpContextAccessor;
        protected readonly string? url;

        protected BaseHandler(IServiceProvider serviceProvider) {
            this.mediator = serviceProvider.GetRequiredService<IMediator>();
            this.db = serviceProvider.GetRequiredService<CafeShopContext>();
            this.configuration = serviceProvider.GetRequiredService<IConfiguration>();
            this.httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();

            this.url = UrlHelper.GetCurrentUrl(httpContextAccessor.HttpContext?.Request, this.configuration, "ImageUrl");
        }

        protected static async Task<T> LockActionByKey<T>(string key, Func<Task<T>> action, int expirySec = 60) {
            using var locker = await new Locker(key, expirySec).Lock();
            return await action.Invoke();
        }
    }

    public abstract class BaseHandler<TRequest> : BaseHandler, IRequestHandler<TRequest>
        where TRequest : IRequest {

        protected BaseHandler(IServiceProvider serviceProvider) : base(serviceProvider) {
        }

        public abstract Task Handle(TRequest request, CancellationToken cancellationToken);
    }

    public abstract class BaseHandler<TRequest, TResponse> : BaseHandler, IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse> {

        protected BaseHandler(IServiceProvider serviceProvider) : base(serviceProvider) {
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}