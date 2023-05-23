using MediatR;
using Newtonsoft.Json;

namespace CafeShop.Common.Models {
    public abstract class BaseRequest {

        [JsonIgnore]
        public Guid MerchantId { get; set; }

        [JsonIgnore]
        public Guid UserId { get; set; }
    }

    public abstract class BaseReq : BaseRequest, IRequest {
    }

    public abstract class BaseReq<TResponse> : BaseRequest, IRequest<TResponse> {
    }
}