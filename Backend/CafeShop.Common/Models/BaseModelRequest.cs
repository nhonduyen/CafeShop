using MediatR;

namespace CafeShop.Common.Models {
    public abstract class BaseModelRequest<TDto> : BaseRequest
        where TDto : notnull {
        public TDto Model { get; set; } = default!;
    }

    public class BaseModelReq<TDto> : BaseModelRequest<TDto>, IRequest
        where TDto : notnull {
    }

    public class BaseModelReq<TDto, TResponse> : BaseModelRequest<TDto>, IRequest<TResponse>
        where TDto : notnull {
    }
}