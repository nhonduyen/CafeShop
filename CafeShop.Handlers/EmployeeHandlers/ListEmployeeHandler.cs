using CafeShop.Common.Models;
using CafeShop.Database.Models;
using CafeShop.Models;
using CafeShop.Models.Dto;
using CafeShop.Models.Mappers;
using CafeShop.Ultilities.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CafeShop.Handlers.EmployeeHandlers {
    public class ListEmployeeReq : BaseListReq<ListEmployeeData> {
        public bool IsActive { get; set; }
    }

    public class ListEmployeeData : BaseListData<EmployeeDto> { }

    public class ListEmployeeHandler : BaseHandler<ListEmployeeReq, ListEmployeeData> {
        private readonly IMapper<EmployeeDto, Employee> mapper;

        public ListEmployeeHandler(IServiceProvider serviceProvider) : base(serviceProvider) {
            this.mapper = new EmployeeMapper();
        }

        public override async Task<ListEmployeeData> Handle(ListEmployeeReq request, CancellationToken cancellationToken) {
            var query = this.db.Employees.AsNoTracking()
                .Where(o => o.MerchantId == request.MerchantId && !o.IsDelete)
                .WhereIf(request.IsActive, o => o.IsActive == request.IsActive)
                .WhereSearch(request.SearchText, o => o.Name.Contains(request.SearchText));

            var entities = await query
                .WhereFunc(!request.IsAll, q => q.OrderBy(o => o.Name).Skip(request.Skip).Take(request.Take))
                .ToListAsync(cancellationToken);

            return new ListEmployeeData {
                Items = this.mapper.FromEntities(entities),
                Count = await query.CountIf(request.IsCount, o => o.Id, cancellationToken),
            };
        }
    }
}