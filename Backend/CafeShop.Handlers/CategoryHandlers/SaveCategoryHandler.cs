using CafeShop.Common.Exceptions;
using CafeShop.Common.Models;
using CafeShop.Database.Models;
using CafeShop.Models;
using CafeShop.Models.Dto;
using CafeShop.Models.Mappers;
using Microsoft.EntityFrameworkCore;

namespace CafeShop.Handlers.CategoryHandlers {
    public class SaveCategoryReq : BaseModelReq<CategoryDto> { }

    public class SaveCategoryHandler : BaseHandler<SaveCategoryReq> {
        private readonly IMapper<CategoryDto, Category> mapper;

        public SaveCategoryHandler(IServiceProvider serviceProvider) : base(serviceProvider) {
            this.mapper = new CategoryMapper();
        }

        public override async Task Handle(SaveCategoryReq request, CancellationToken cancellationToken) {
            if (request.Model.Id == Guid.Empty) {
                await this.Create(request, cancellationToken);
            } else {
                await this.Update(request, cancellationToken);
            }
        }

        private async Task Create(SaveCategoryReq request, CancellationToken cancellationToken) {
            var entity = this.mapper.ToEntity(request);
            await this.db.Categories.AddAsync(entity, cancellationToken);
            await this.db.SaveChangesAsync(cancellationToken);
        }

        private async Task Update(SaveCategoryReq request, CancellationToken cancellationToken) {
            var other = this.mapper.ToEntity(request);

            var entity = await this.db.Categories.FirstOrDefaultAsync(o => o.MerchantId == request.MerchantId && o.Id == other.Id && !o.IsDelete, cancellationToken);
            ManagedException.ThrowIfNull(entity, "Không tìm thấy danh mục.");

            entity.Update(other);
            await this.db.SaveChangesAsync(cancellationToken);
        }
    }
}