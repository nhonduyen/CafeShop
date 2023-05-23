using CafeShop.Common.Models;
using CafeShop.Database.Models;
using CafeShop.Models;
using CafeShop.Models.Dto;
using CafeShop.Models.Mappers;
using CafeShop.Ultilities.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CafeShop.Handlers.TableHandlers {
    public class ListTableReq : BaseListReq<ListTableData> {
        public bool IsActive { get; set; }
    }

    public class ListTableData : BaseListData<TableDto> { }

    public class ListTableHandler : BaseHandler<ListTableReq, ListTableData> {
        private readonly IMapper<TableDto, Table> mapper;

        public ListTableHandler(IServiceProvider serviceProvider) : base(serviceProvider) {
            this.mapper = new TableMapper();
        }

        public override async Task<ListTableData> Handle(ListTableReq request, CancellationToken cancellationToken) {
            var query = this.db.Tables.AsNoTracking()
                .Where(o => o.MerchantId == request.MerchantId && !o.IsDelete)
                .WhereIf(request.IsActive, o => o.IsActive == request.IsActive)
                .WhereSearch(request.SearchText, o => o.Name.Contains(request.SearchText));

            var entities = await query
                .WhereFunc(!request.IsAll, q => q.OrderBy(o => o.Name).Skip(request.Skip).Take(request.Take))
                .ToListAsync(cancellationToken);

            return new ListTableData {
                Items = this.mapper.FromEntities(entities),
                Count = await query.CountIf(request.IsCount, o => o.Id, cancellationToken),
            };
        }
    }
}