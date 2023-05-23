using CafeShop.Common.Exceptions;
using CafeShop.Common.Models;
using CafeShop.Database.Models;
using CafeShop.Models;
using CafeShop.Models.Dto;
using CafeShop.Models.Mappers;
using Microsoft.EntityFrameworkCore;

namespace CafeShop.Handlers.CategoryHandlers {
    public class GetCategoryReq : BaseSingleReq<CategoryDto> {
    }

    public class GetCategoryHandler : BaseHandler<GetCategoryReq, CategoryDto> {
        private readonly IMapper<CategoryDto, Category> mapper;

        public GetCategoryHandler(IServiceProvider serviceProvider) : base(serviceProvider) {
            this.mapper = new CategoryMapper();
        }

        public override async Task<CategoryDto> Handle(GetCategoryReq request, CancellationToken cancellationToken) {
            var entity = await this.db.Categories.AsNoTracking()
                .Where(o => o.MerchantId == request.MerchantId && o.Id == request.Id && !o.IsDelete)
                .FirstOrDefaultAsync(cancellationToken);
            ManagedException.ThrowIfNull(entity, "Không tìm thấy danh mục.");

            return this.mapper.FromEntity(entity);
        }
    }
}