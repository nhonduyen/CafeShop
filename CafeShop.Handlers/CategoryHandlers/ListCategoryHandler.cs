using CafeShop.Common.Models;
using CafeShop.Database.Models;
using CafeShop.Models;
using CafeShop.Models.Dto;
using CafeShop.Models.Mappers;
using CafeShop.Ultilities.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CafeShop.Handlers.CategoryHandlers {
    public class ListCategoryReq : BaseListReq<ListCategoryData> {
        public bool IsActive { get; set; }
    }

    public class ListCategoryData : BaseListData<CategoryDto> { }

    public class ListCategoryHandler : BaseHandler<ListCategoryReq, ListCategoryData> {
        private readonly IMapper<CategoryDto, Category> mapper;

        public ListCategoryHandler(IServiceProvider serviceProvider) : base(serviceProvider) {
            this.mapper = new CategoryMapper();
        }

        public override async Task<ListCategoryData> Handle(ListCategoryReq request, CancellationToken cancellationToken) {
            var query = this.db.Categories.AsNoTracking()
                .Where(o => o.MerchantId == request.MerchantId && !o.IsDelete)
                .WhereIf(request.IsActive, o => o.IsActive == request.IsActive)
                .WhereSearch(request.SearchText, o => o.Name.Contains(request.SearchText));

            var entities = await query
                .WhereFunc(!request.IsAll, q => q.OrderBy(o => o.Name).Skip(request.Skip).Take(request.Take))
                .ToListAsync(cancellationToken);

            return new ListCategoryData {
                Items = this.mapper.FromEntities(entities),
                Count = await query.CountIf(request.IsCount, o => o.Id, cancellationToken),
            };
        }
    }
}