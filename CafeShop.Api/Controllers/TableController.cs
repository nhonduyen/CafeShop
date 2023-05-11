using CafeShop.Controllers;
using CafeShop.Handlers.TableHandlers;
using CafeShop.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeShop.Api.Controllers {
    [ApiController, Route("api/table"), Authorize]
    public class TableController : BaseController {

        protected TableController(IServiceProvider serviceProvider) : base(serviceProvider) {
        }

        [HttpPost("list"), ProducesResponseType(typeof(ListTableData), 200)]
        public async Task<IActionResult> List([FromBody] ListTableReq req) {
            req.MerchantId = this.merchantId;
            req.UserId = this.userId;
            var data = await this.mediator.Send(req);
            return Ok(data);
        }

        [HttpPost("get"), ProducesResponseType(typeof(TableDto), 200)]
        public async Task<IActionResult> Get([FromBody] GetTableReq req) {
            req.MerchantId = this.merchantId;
            req.UserId = this.userId;
            var data = await this.mediator.Send(req);
            return Ok(data);
        }

        [HttpPost("delete"), ProducesResponseType(200)]
        public async Task<IActionResult> Delete([FromBody] DeleteTableReq req) {
            req.MerchantId = this.merchantId;
            req.UserId = this.userId;
            await this.mediator.Send(req);
            return Ok();
        }

        [HttpPost("save"), ProducesResponseType(200)]
        public async Task<IActionResult> Save([FromBody] TableDto model) {
            await this.mediator.Send(new SaveTableReq {
                MerchantId = this.merchantId,
                UserId = this.userId,
                Model = model,
            });
            return Ok();
        }
    }
}