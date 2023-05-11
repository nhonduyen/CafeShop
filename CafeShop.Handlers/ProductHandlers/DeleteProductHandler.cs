using CafeShop.Common.Exceptions;
using CafeShop.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeShop.Handlers.ProductHandlers {
    public class DeleteProductReq : BaseSingleReq { }

    public class DeleteProductHandler : BaseHandler<DeleteProductReq> {

        public DeleteProductHandler(IServiceProvider serviceProvider) : base(serviceProvider) {
        }

        public override async Task Handle(DeleteProductReq request, CancellationToken cancellationToken) {
            var entity = await this.db.Products.FirstOrDefaultAsync(o => o.MerchantId == request.MerchantId && o.Id == request.Id && !o.IsDelete, cancellationToken);
            ManagedException.ThrowIfNull(entity, "Không tìm thấy sản phẩm.");

            entity.Delete(request.UserId);
            await this.db.SaveChangesAsync(cancellationToken);
        }
    }
}