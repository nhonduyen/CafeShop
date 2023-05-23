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
    public class GetProductReq : BaseSingleReq<ProductDto> {
    }

    public class GetProductHandler : BaseHandler<GetProductReq, ProductDto> {
        private readonly IMapper<ProductDto, Product> mapper;
        private readonly ImageService imageService;

        public GetProductHandler(IServiceProvider serviceProvider) : base(serviceProvider) {
            this.mapper = new ProductMapper();
            this.imageService = serviceProvider.GetRequiredService<ImageService>();
        }

        public override async Task<ProductDto> Handle(GetProductReq request, CancellationToken cancellationToken) {
            var entity = await this.db.Products.AsNoTracking()
                .Include(o => o.Category)
                .Where(o => o.MerchantId == request.MerchantId && o.Id == request.Id && !o.IsDelete)
                .FirstOrDefaultAsync(cancellationToken);
            ManagedException.ThrowIfNull(entity, "Không tìm thấy danh mục.");

            var image = await this.imageService.Get(request.MerchantId, EImage.Product, entity.Id, cancellationToken);

            return this.mapper.FromEntity(entity).LoadImage(image, this.url);
        }
    }
}