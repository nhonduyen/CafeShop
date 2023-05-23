using CafeShop.Controllers;
using CafeShop.Handlers.ProductHandlers;
using CafeShop.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeShop.Api.Controllers {
    [ApiController, Route("api/product"), Authorize]
    public class ProductController : BaseController {

        protected ProductController(IServiceProvider serviceProvider) : base(serviceProvider) {
        }

        [HttpPost("list"), ProducesResponseType(typeof(ListProductData), 200)]
        public async Task<IActionResult> List([FromBody] ListProductReq req) {
            req.MerchantId = this.merchantId;
            req.UserId = this.userId;
            var data = await this.mediator.Send(req);
            return Ok(data);
        }

        [HttpPost("get"), ProducesResponseType(typeof(ProductDto), 200)]
        public async Task<IActionResult> Get([FromBody] GetProductReq req) {
            req.MerchantId = this.merchantId;
            req.UserId = this.userId;
            var data = await this.mediator.Send(req);
            return Ok(data);
        }

        [HttpPost("delete"), ProducesResponseType(200)]
        public async Task<IActionResult> Delete([FromBody] DeleteProductReq req) {
            req.MerchantId = this.merchantId;
            req.UserId = this.userId;
            await this.mediator.Send(req);
            return Ok();
        }

        [HttpPost("save"), ProducesResponseType(200)]
        public async Task<IActionResult> Save([FromBody] ProductDto model) {
            await this.mediator.Send(new SaveProductReq {
                MerchantId = this.merchantId,
                UserId = this.userId,
                Model = model,
            });
            return Ok();
        }
    }
}