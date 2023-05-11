using CafeShop.Controllers;
using CafeShop.Handlers.AuthHandlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeShop.Api.Controllers {
    [ApiController, Route("api/auth"), AllowAnonymous]
    public class AuthController : BaseController {

        protected AuthController(IServiceProvider serviceProvider) : base(serviceProvider) {
        }

        [HttpPost, ProducesResponseType(typeof(LoginRes), 200)]
        public async Task<IActionResult> Login([FromBody] LoginReq req) {
            if (string.IsNullOrWhiteSpace(req.MerchantCode)) {
                req.MerchantCode = this.configuration.GetValue<string>("DefaultMerchantCode");
            }

            var data = await this.mediator.Send(req);
            return Ok(data);
        }
    }
}