using CafeShop.Controllers;
using CafeShop.Handlers.CategoryHandlers;
using CafeShop.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeShop.Api.Controllers {
    [ApiController, Route("api/category"), Authorize]
    public class CategoryController : BaseController {

        protected CategoryController(IServiceProvider serviceProvider) : base(serviceProvider) {
        }

        [HttpPost("list"), ProducesResponseType(typeof(ListCategoryData), 200)]
        public async Task<IActionResult> List([FromBody] ListCategoryReq req) {
            req.MerchantId = this.merchantId;
            req.UserId = this.userId;
            var data = await this.mediator.Send(req);
            return Ok(data);
        }

        [HttpPost("get"), ProducesResponseType(typeof(CategoryDto), 200)]
        public async Task<IActionResult> Get([FromBody] GetCategoryReq req) {
            req.MerchantId = this.merchantId;
            req.UserId = this.userId;
            var data = await this.mediator.Send(req);
            return Ok(data);
        }

        [HttpPost("delete"), ProducesResponseType(200)]
        public async Task<IActionResult> Delete([FromBody] DeleteCategoryReq req) {
            req.MerchantId = this.merchantId;
            req.UserId = this.userId;
            await this.mediator.Send(req);
            return Ok();
        }

        [HttpPost("save"), ProducesResponseType(200)]
        public async Task<IActionResult> Save([FromBody] CategoryDto model) {
            await this.mediator.Send(new SaveCategoryReq {
                MerchantId = this.merchantId,
                UserId = this.userId,
                Model = model,
            });
            return Ok();
        }
    }
}