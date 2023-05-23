using CafeShop.Common.Exceptions;
using CafeShop.Common.Models;
using CafeShop.Database.Models;
using CafeShop.Models;
using CafeShop.Models.Dto;
using CafeShop.Models.Mappers;
using CafeShop.Ultilities.Hashers;
using Microsoft.EntityFrameworkCore;

namespace CafeShop.Handlers.EmployeeHandlers {
    public class SaveEmployeeReq : BaseModelReq<EmployeeDto> { }

    public class SaveEmployeeHandler : BaseHandler<SaveEmployeeReq> {
        private readonly IMapper<EmployeeDto, Employee> mapper;

        public SaveEmployeeHandler(IServiceProvider serviceProvider) : base(serviceProvider) {
            this.mapper = new EmployeeMapper();
        }

        public override async Task Handle(SaveEmployeeReq request, CancellationToken cancellationToken) {
            if (request.Model.Id == Guid.Empty) {
                await this.Create(request, cancellationToken);
            } else {
                await this.Update(request, cancellationToken);
            }
        }

        private async Task Create(SaveEmployeeReq request, CancellationToken cancellationToken) {
            var entity = this.mapper.ToEntity(request);
            entity.Password = PasswordHasher.Hash(request.Model.Password);
            await this.db.Employees.AddAsync(entity, cancellationToken);
            await this.db.SaveChangesAsync(cancellationToken);
        }

        private async Task Update(SaveEmployeeReq request, CancellationToken cancellationToken) {
            var other = this.mapper.ToEntity(request);

            var entity = await this.db.Employees.FirstOrDefaultAsync(o => o.MerchantId == request.MerchantId && o.Id == other.Id && !o.IsDelete, cancellationToken);
            ManagedException.ThrowIfNull(entity, "Không tìm thấy nhân viên.");

            entity.Update(other);
            await this.db.SaveChangesAsync(cancellationToken);
        }
    }
}