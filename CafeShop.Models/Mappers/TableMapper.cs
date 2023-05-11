using CafeShop.Common.Models;
using CafeShop.Database.Models;
using CafeShop.Models.Dto;
using CafeShop.Ultilities.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace CafeShop.Models.Mappers {
    public class TableMapper : IMapper<TableDto, Table> {

        [return: NotNull]
        public TableDto FromEntity(Table entity) {
            return new TableDto {
                Id = entity.Id,
                Name = entity.Name,
                IsActive = entity.IsActive,
            };
        }

        [return: NotNull]
        public Table ToEntity(BaseModelReq<TableDto> req) {
            return new Table {
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