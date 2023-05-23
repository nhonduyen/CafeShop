using CafeShop.Common.Models;
using CafeShop.Database.Models;
using CafeShop.Models.Dto;
using CafeShop.Ultilities.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace CafeShop.Models.Mappers {
    public class ProductMapper : IMapper<ProductDto, Product> {

        [return: NotNull]
        public ProductDto FromEntity(Product entity) {
            return new ProductDto {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                IsActive = entity.IsActive,
                Category = new CategoryMapper().FromEntity(entity.Category!),
            };
        }

        [return: NotNull]
        public Product ToEntity(BaseModelReq<ProductDto> req) {
            return new Product {
                Id = req.Model.Id.NewIfEmpty(),
                MerchantId = req.MerchantId,
                CategoryId = req.Model.Category.Id,
                Name = req.Model.Name,
                Price = req.Model.Price,
                IsActive = req.Model.IsActive,
                IsDelete = false,
                ModifiedBy = req.UserId,
                ModifiedDate = DateTimeOffset.UtcNow,
            };
        }
    }
}