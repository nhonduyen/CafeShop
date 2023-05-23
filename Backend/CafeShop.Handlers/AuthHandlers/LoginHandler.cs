using CafeShop.Common;
using CafeShop.Common.Exceptions;
using CafeShop.Ultilities.Hashers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CafeShop.Handlers.AuthHandlers {
    public class LoginReq : IRequest<LoginRes> {
        public string MerchantCode { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginRes {
        public string Token { get; set; } = string.Empty;
        public string MerchantCode { get; set; } = string.Empty;
        public string MerchantName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public long ExpiredTime { get; set; }
    }

    public class LoginHandler : BaseHandler<LoginReq, LoginRes> {

        public LoginHandler(IServiceProvider serviceProvider) : base(serviceProvider) {
        }

        public override async Task<LoginRes> Handle(LoginReq request, CancellationToken cancellationToken) {
            var merchant = await this.db.Merchants.AsNoTracking().FirstOrDefaultAsync(o => o.Code == request.MerchantCode, cancellationToken);
            ManagedException.ThrowIfNull(merchant, "Không tìm thấy cửa hàng.");
            ManagedException.ThrowIfFalse(merchant.IsActive, "Cửa hàng đang không hoạt động. Vui lòng liên hệ bộ phần CSKH.");
            ManagedException.ThrowIf(merchant.ExpiredDate <= DateTimeOffset.UtcNow, "Cửa hàng đã hết hạn. Vui lòng liên hệ bộ phần CSKH.");

            var employee = await this.db.Employees.AsNoTracking()
                .Where(o => o.MerchantId == merchant.Id && o.Username == request.Username.ToLower().Trim())
                .FirstOrDefaultAsync(cancellationToken);
            ManagedException.ThrowIfNull(employee, "Không tìm thấy nhân viên.");
            ManagedException.ThrowIf(!employee.IsOwner && !employee.IsActive, "Nhân viên này không còn hoạt động.");
            ManagedException.ThrowIf(!PasswordHasher.Verify(request.Password, employee.Password), "Sai mật khẩu.");

            var expiredAt = DateTime.Now.AddDays(7);

            return new() {
                Token = this.GenerateToken(merchant.Id, employee.Id, expiredAt),
                ExpiredTime = new DateTimeOffset(expiredAt).ToUnixTimeMilliseconds(),
                MerchantCode = merchant.Code,
                MerchantName = merchant.Name,
                Username = employee.Username,
                Name = employee.Name,
            };
        }

        private string GenerateToken(Guid merchantId, Guid employeeId, DateTime expiredAt) {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["JwtSecret"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>() {
                new Claim(Constants.TokenMerchantId, merchantId.ToString()),
                new Claim(Constants.TokenUserId, employeeId.ToString()),
            };

            var token = new JwtSecurityToken(
              claims: claims,
              expires: expiredAt,
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}