using CafeShop.Common.Models;
using CafeShop.Database.Models;
using CafeShop.Models.Dto;
using CafeShop.Ultilities.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace CafeShop.Models.Mappers {
    public class CategoryMapper : IMapper<CategoryDto, Category> {

        [return: NotNull]
        public CategoryDto FromEntity(Category entity) {
            return new CategoryDto {
                Id = entity.Id,
                Name = entity.Name,
                IsActive = entity.IsActive,
            };
        }

        [return: NotNull]
        public Category ToEntity(BaseModelReq<CategoryDto> req) {
            return new Category {
                Id = req.Model.Id.NewIfEmpty(),
                MerchantId = req.MerchantId,
                Name = req.Model.Name,
                IsActive = req.Model.IsActive,
                IsDelete = false,
                ModifiedBy = req.UserId,
                ModifiedDate = DateTimeOffset.UtcNow,
            };
        }
    }
}