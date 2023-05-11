using CafeShop.Common.Exceptions;
using CafeShop.Common.Models;
using CafeShop.Database.Models;
using CafeShop.Models;
using CafeShop.Models.Dto;
using CafeShop.Models.Mappers;
using Microsoft.EntityFrameworkCore;

namespace CafeShop.Handlers.TableHandlers {
    public class GetTableReq : BaseSingleReq<TableDto> {
    }

    public class GetTableHandler : BaseHandler<GetTableReq, TableDto> {
        private readonly IMapper<TableDto, Table> mapper;

        public GetTableHandler(IServiceProvider serviceProvider) : base(serviceProvider) {
            this.mapper = new TableMapper();
        }

        public override async Task<TableDto> Handle(GetTableReq request, CancellationToken cancellationToken) {
            var entity = await this.db.Tables.AsNoTracking()
                .Where(o => o.MerchantId == request.MerchantId && o.Id == request.Id && !o.IsDelete)
                .FirstOrDefaultAsync(cancellationToken);
            ManagedException.ThrowIfNull(entity, "Không tìm thấy bàn.");

            return this.mapper.FromEntity(entity);
        }
    }
}