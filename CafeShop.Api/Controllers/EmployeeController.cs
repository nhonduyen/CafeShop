using CafeShop.Controllers;
using CafeShop.Handlers.EmployeeHandlers;
using CafeShop.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeShop.Api.Controllers {
    [ApiController, Route("api/employee"), Authorize]
    public class EmployeeController : BaseController {

        protected EmployeeController(IServiceProvider serviceProvider) : base(serviceProvider) {
        }

        [HttpPost("list"), ProducesResponseType(typeof(ListEmployeeData), 200)]
        public async Task<IActionResult> List([FromBody] ListEmployeeReq req) {
            req.MerchantId = this.merchantId;
            req.UserId = this.userId;
            var data = await this.mediator.Send(req);
            return Ok(data);
        }

        [HttpPost("get"), ProducesResponseType(typeof(EmployeeDto), 200)]
        public async Task<IActionResult> Get([FromBody] GetEmployeeReq req) {
            req.MerchantId = this.merchantId;
            req.UserId = this.userId;
            var data = await this.mediator.Send(req);
            return Ok(data);
        }

        [HttpPost("current"), ProducesResponseType(typeof(EmployeeDto), 200)]
        public async Task<IActionResult> Current() {
            var data = await this.mediator.Send(new GetEmployeeReq {
                MerchantId = this.merchantId,
                UserId = this.userId,
                Id = this.userId,
            });
            return Ok(data);
        }

        [HttpPost("delete"), ProducesResponseType(200)]
        public async Task<IActionResult> Delete([FromBody] DeleteEmployeeReq req) {
            req.MerchantId = this.merchantId;
            req.UserId = this.userId;
            await this.mediator.Send(req);
            return Ok();
        }

        [HttpPost("save"), ProducesResponseType(200)]
        public async Task<IActionResult> Save([FromBody] EmployeeDto model) {
            await this.mediator.Send(new SaveEmployeeReq {
                MerchantId = this.merchantId,
                UserId = this.userId,
                Model = model,
            });
            return Ok();
        }

        [HttpPost("change-password"), ProducesResponseType(200)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordReq req) {
            req.MerchantId = this.merchantId;
            req.UserId = this.userId;
            await this.mediator.Send(req);
            return Ok();
        }
    }
}