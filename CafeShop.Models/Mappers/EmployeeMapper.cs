using CafeShop.Common.Models;
using CafeShop.Database.Models;
using CafeShop.Models.Dto;
using CafeShop.Ultilities.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace CafeShop.Models.Mappers {
    public class EmployeeMapper : IMapper<EmployeeDto, Employee> {

        [return: NotNull]
        public EmployeeDto FromEntity(Employee entity) {
            return new EmployeeDto {
                Id = entity.Id,
                Username = entity.Username,
                Name = entity.Name,
                IsActive = entity.IsActive,
            };
        }

        [return: NotNull]
        public Employee ToEntity(BaseModelReq<EmployeeDto> req) {
            return new Employee {
                Id = req.Model.Id.NewIfEmpty(),
                MerchantId = req.MerchantId,
                Username = req.Model.Username,
                Name = req.Model.Name,
                IsActive = req.Model.IsActive,
                IsOwner = false,
                IsDelete = false,
                ModifiedBy = req.UserId,
                ModifiedDate = DateTimeOffset.UtcNow,
            };
        }
    }
}