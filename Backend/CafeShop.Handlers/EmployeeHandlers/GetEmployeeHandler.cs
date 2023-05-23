using CafeShop.Common.Exceptions;
using CafeShop.Common.Models;
using CafeShop.Database.Models;
using CafeShop.Models;
using CafeShop.Models.Dto;
using CafeShop.Models.Mappers;
using Microsoft.EntityFrameworkCore;

namespace CafeShop.Handlers.EmployeeHandlers {
    public class GetEmployeeReq : BaseSingleReq<EmployeeDto> {
    }

    public class GetEmployeeHandler : BaseHandler<GetEmployeeReq, EmployeeDto> {
        private readonly IMapper<EmployeeDto, Employee> mapper;

        public GetEmployeeHandler(IServiceProvider serviceProvider) : base(serviceProvider) {
            this.mapper = new EmployeeMapper();
        }

        public override async Task<EmployeeDto> Handle(GetEmployeeReq request, CancellationToken cancellationToken) {
            var entity = await this.db.Employees.AsNoTracking()
                .Where(o => o.MerchantId == request.MerchantId && o.Id == request.Id && !o.IsDelete)
                .FirstOrDefaultAsync(cancellationToken);
            ManagedException.ThrowIfNull(entity, "Không tìm thấy nhân viên.");

            return this.mapper.FromEntity(entity);
        }
    }
}