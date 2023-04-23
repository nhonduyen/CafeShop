using MediatR;

namespace CafeShop.Common.Models {
    public abstract class BaseSingleRequest : BaseRequest {
        public string Id { get; set; } = string.Empty;
    }

    public class BaseSingleReq : BaseSingleRequest, IRequest {
    }

    public class BaseSingleReq<TResponse> : BaseSingleRequest, IRequest<TResponse> {
    }
}