using CafeShop.Common.Exceptions;
using CafeShop.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeShop.Handlers.TableHandlers {
    public class DeleteTableReq : BaseSingleReq { }

    public class DeleteTableHandler : BaseHandler<DeleteTableReq> {

        public DeleteTableHandler(IServiceProvider serviceProvider) : base(serviceProvider) {
        }

        public override async Task Handle(DeleteTableReq request, CancellationToken cancellationToken) {
            var entity = await this.db.Tables.FirstOrDefaultAsync(o => o.MerchantId == request.MerchantId && o.Id == request.Id && !o.IsDelete, cancellationToken);
            ManagedException.ThrowIfNull(entity, "Không tìm thấy bàn.");

            entity.Delete(request.UserId);
            await this.db.SaveChangesAsync(cancellationToken);
        }
    }
}