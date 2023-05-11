using MediatR;

namespace CafeShop.Common.Models {
    public abstract class BaseSingleRequest : BaseRequest {
        public Guid Id { get; set; }
    }

    public class BaseSingleReq : BaseSingleRequest, IRequest {
    }

    public class BaseSingleReq<TResponse> : BaseSingleRequest, IRequest<TResponse> {
    }
}