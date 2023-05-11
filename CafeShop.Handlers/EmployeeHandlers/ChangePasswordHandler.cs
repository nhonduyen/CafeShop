using CafeShop.Common.Exceptions;
using CafeShop.Common.Models;
using CafeShop.Ultilities.Hashers;
using Microsoft.EntityFrameworkCore;

namespace CafeShop.Handlers.EmployeeHandlers {
    public class ChangePasswordReq : BaseReq {
        public string OldPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }

    public class ChangePasswordHandler : BaseHandler<ChangePasswordReq> {

        public ChangePasswordHandler(IServiceProvider serviceProvider) : base(serviceProvider) {
        }

        public override async Task Handle(ChangePasswordReq request, CancellationToken cancellationToken) {
            var entity = await this.db.Employees.FirstOrDefaultAsync(o => o.MerchantId == request.MerchantId && o.Id == request.UserId && !o.IsDelete && !o.IsOwner, cancellationToken);
            ManagedException.ThrowIfNull(entity, "Không tìm thấy nhân viên.");
            ManagedException.ThrowIf(!PasswordHasher.Verify(request.OldPassword, entity.Password), "Sai mật khẩu.");

            entity.Password = PasswordHasher.Hash(request.NewPassword);
            entity.ModifiedBy = request.UserId;
            entity.ModifiedDate = DateTimeOffset.UtcNow;

            await this.db.SaveChangesAsync(cancellationToken);
        }
    }
}