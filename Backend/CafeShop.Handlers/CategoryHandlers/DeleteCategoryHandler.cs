using CafeShop.Common.Exceptions;
using CafeShop.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeShop.Handlers.CategoryHandlers {
    public class DeleteCategoryReq : BaseSingleReq { }

    public class DeleteCategoryHandler : BaseHandler<DeleteCategoryReq> {

        public DeleteCategoryHandler(IServiceProvider serviceProvider) : base(serviceProvider) {
        }

        public override async Task Handle(DeleteCategoryReq request, CancellationToken cancellationToken) {
            var entity = await this.db.Categories.FirstOrDefaultAsync(o => o.MerchantId == request.MerchantId && o.Id == request.Id && !o.IsDelete, cancellationToken);
            ManagedException.ThrowIfNull(entity, "Không tìm thấy danh mục.");

            var existedProduct = await this.db.Products.AsNoTracking()
                .Where(o => o.MerchantId == request.MerchantId && o.CategoryId == entity.Id && !o.IsDelete)
                .Select(o => o.Id)
                .AnyAsync(cancellationToken);
            ManagedException.ThrowIf(existedProduct, "Không thể xoá danh mục đang sử dụng.");

            entity.Delete(request.UserId);
            await this.db.SaveChangesAsync(cancellationToken);
        }
    }
}