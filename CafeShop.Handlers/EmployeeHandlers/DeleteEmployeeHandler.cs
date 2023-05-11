using CafeShop.Common.Exceptions;
using CafeShop.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeShop.Handlers.EmployeeHandlers {
    public class DeleteEmployeeReq : BaseSingleReq { }

    public class DeleteEmployeeHandler : BaseHandler<DeleteEmployeeReq> {

        public DeleteEmployeeHandler(IServiceProvider serviceProvider) : base(serviceProvider) {
        }

        public override async Task Handle(DeleteEmployeeReq request, CancellationToken cancellationToken) {
            var entity = await this.db.Employees.FirstOrDefaultAsync(o => o.MerchantId == request.MerchantId && o.Id == request.Id && !o.IsDelete && !o.IsOwner, cancellationToken);
            ManagedException.ThrowIfNull(entity, "Không tìm thấy nhân viên.");

            entity.Delete(request.UserId);
            await this.db.SaveChangesAsync(cancellationToken);
        }
    }
}