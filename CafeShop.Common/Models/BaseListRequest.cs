using MediatR;

namespace CafeShop.Common.Models {
    public abstract class BaseListRequest : BaseRequest {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public bool IsCount { get; set; }
        public string? SearchText { get; set; }
        public string? FirstItemId { get; set; }

        public int Skip => this.PageIndex * this.PageSize;
        public int Take => this.PageSize;
    }

    public class BaseListReq : BaseListRequest, IRequest<Unit> {
    }

    public class BaseListReq<TResponse> : BaseListRequest, IRequest<TResponse> {
    }
}