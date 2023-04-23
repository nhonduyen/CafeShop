using MediatR;

namespace CafeShop.Common.Models {
    public abstract class BaseRequest {
        public string MerchantId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }

    public abstract class BaseReq : BaseRequest, IRequest {
    }

    public abstract class BaseReq<TResponse> : BaseRequest, IRequest<TResponse> {
    }
}