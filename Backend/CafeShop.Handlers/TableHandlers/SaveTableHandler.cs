using CafeShop.Common.Exceptions;
using CafeShop.Common.Models;
using CafeShop.Database.Models;
using CafeShop.Models;
using CafeShop.Models.Dto;
using CafeShop.Models.Mappers;
using Microsoft.EntityFrameworkCore;

namespace CafeShop.Handlers.TableHandlers {
    public class SaveTableReq : BaseModelReq<TableDto> { }

    public class SaveTableHandler : BaseHandler<SaveTableReq> {
        private readonly IMapper<TableDto, Table> mapper;

        public SaveTableHandler(IServiceProvider serviceProvider) : base(serviceProvider) {
            this.mapper = new TableMapper();
        }

        public override async Task Handle(SaveTableReq request, CancellationToken cancellationToken) {
            if (request.Model.Id == Guid.Empty) {
                await this.Create(request, cancellationToken);
            } else {
                await this.Update(request, cancellationToken);
            }
        }

        private async Task Create(SaveTableReq request, CancellationToken cancellationToken) {
            var entity = this.mapper.ToEntity(request);
            await this.db.Tables.AddAsync(entity, cancellationToken);
            await this.db.SaveChangesAsync(cancellationToken);
        }

        private async Task Update(SaveTableReq request, CancellationToken cancellationToken) {
            var other = this.mapper.ToEntity(request);

            var entity = await this.db.Tables.FirstOrDefaultAsync(o => o.MerchantId == request.MerchantId && o.Id == other.Id && !o.IsDelete, cancellationToken);
            ManagedException.ThrowIfNull(entity, "Không tìm thấy bàn.");

            entity.Update(other);
            await this.db.SaveChangesAsync(cancellationToken);
        }
    }
}