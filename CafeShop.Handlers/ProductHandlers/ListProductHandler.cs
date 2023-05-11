using CafeShop.Common.Models;
using CafeShop.CommonServices;
using CafeShop.Database.Enums;
using CafeShop.Database.Models;
using CafeShop.Models;
using CafeShop.Models.Dto;
using CafeShop.Models.Mappers;
using CafeShop.Ultilities.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CafeShop.Handlers.ProductHandlers {
    public class ListProductReq : BaseListReq<ListProductData> {
        public bool IsActive { get; set; }
    }

    public class ListProductData : BaseListData<ProductDto> { }

    public class ListProductHandler : BaseHandler<ListProductReq, ListProductData> {
        private readonly IMapper<ProductDto, Product> mapper;
        private readonly ImageService imageService;

        public ListProductHandler(IServiceProvider serviceProvider) : base(serviceProvider) {
            this.mapper = new ProductMapper();
            this.imageService = serviceProvider.GetRequiredService<ImageService>();
        }

        public override async Task<ListProductData> Handle(ListProductReq request, CancellationToken cancellationToken) {
            var query = this.db.Products.AsNoTracking()
                .Include(o => o.Category)
                .Where(o => o.MerchantId == request.MerchantId && !o.IsDelete)
                .WhereIf(request.IsActive, o => o.IsActive == request.IsActive)
                .WhereSearch(request.SearchText, o => o.Name.Contains(request.SearchText));

            var entities = await query
                .WhereFunc(!request.IsAll, q => q.OrderBy(o => o.Name).Skip(request.Skip).Take(request.Take))
                .ToListAsync(cancellationToken);

            var entityIds = entities.Select(o => o.Id).ToList();
            var images = await this.imageService.List(request.MerchantId, EImage.Product, entityIds, cancellationToken);

            return new ListProductData {
                Items = this.mapper.FromEntities(entities).Select(o => o.LoadImage(images.FirstOrDefault(o => o.ItemId == o.Id), this.url)).ToList(),
                Count = await query.CountIf(request.IsCount, o => o.Id, cancellationToken),
            };
        }
    }
}