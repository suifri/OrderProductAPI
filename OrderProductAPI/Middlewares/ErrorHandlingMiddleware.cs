using Microsoft.AspNetCore.Http;
using System.Net;

namespace OrderProductAPI.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext contenxt)
        {
            try
            {
                await _next(contenxt);
            }
            catch (Exception ex)
            {
                contenxt.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await contenxt.Response.WriteAsync(ex.Message);
            }
        }
    }
}
