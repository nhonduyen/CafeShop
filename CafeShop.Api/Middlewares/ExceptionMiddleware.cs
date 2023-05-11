using CafeShop.Common.Exceptions;
using CafeShop.Common.Models;
using System.Net;

namespace CafeShop.Middlewares {
    public class ExceptionMiddleware {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next) {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context) {
            try {
                await this.next(context);
            } catch (ManagedException ex) {
                await HandleException(context, (int)HttpStatusCode.BadRequest, ex);
            } catch (Exception ex) {
                await HandleException(context, (int)HttpStatusCode.InternalServerError, ex);
            }
        }

        private static async Task HandleException(HttpContext context, int statusCode, Exception exception) {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(BaseRes.Fail(exception.Message).ToString());
        }
    }
}