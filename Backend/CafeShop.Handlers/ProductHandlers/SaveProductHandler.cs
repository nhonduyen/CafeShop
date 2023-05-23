using CafeShop.Common.Exceptions;
using CafeShop.Common.Models;
using CafeShop.CommonServices;
using CafeShop.Database.Enums;
using CafeShop.Database.Models;
using CafeShop.Models;
using CafeShop.Models.Dto;
using CafeShop.Models.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CafeShop.Handlers.ProductHandlers {
    public class SaveProductReq : BaseModelReq<ProductDto> { }

    public class SaveProductHandler : BaseHandler<SaveProductReq> {
        private readonly IMapper<ProductDto, Product> mapper;
        private readonly ImageService imageService;

        public SaveProductHandler(IServiceProvider serviceProvider) : base(serviceProvider) {
            this.mapper = new ProductMapper();
            this.imageService = serviceProvider.GetRequiredService<ImageService>();
        }

        public override async Task Handle(SaveProductReq request, CancellationToken cancellationToken) {
            if (request.Model.Id == Guid.Empty) {
                await this.Create(request, cancellationToken);
            } else {
                await this.Update(request, cancellationToken);
            }
        }

        private async Task Create(SaveProductReq request, CancellationToken cancellationToken) {
            var entity = this.mapper.ToEntity(request);
            await this.imageService.Save(request.MerchantId, request.UserId, EImage.Product, entity.Id, request.Model.Image!, cancellationToken: cancellationToken);
            await this.db.Products.AddAsync(entity, cancellationToken);
            await this.db.SaveChangesAsync(cancellationToken);
        }

        private async Task Update(SaveProductReq request, CancellationToken cancellationToken) {
            var other = this.mapper.ToEntity(request);

            var entity = await this.db.Products.FirstOrDefaultAsync(o => o.MerchantId == request.MerchantId && o.Id == other.Id && !o.IsDelete, cancellationToken);
            ManagedException.ThrowIfNull(entity, "Không tìm thấy sản phẩm.");

            await this.imageService.Save(request.MerchantId, request.UserId, EImage.Product, entity.Id, request.Model.Image!, cancellationToken: cancellationToken);
            entity.Update(other);
            await this.db.SaveChangesAsync(cancellationToken);
        }
    }
}