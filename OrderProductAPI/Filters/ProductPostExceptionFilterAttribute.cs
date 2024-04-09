using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace OrderProductAPI.Filters
{
    public class ProductPostExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.Result = new ObjectResult("Error on Post action")
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Value = "https://tools.ietf.org/html/rfc7231#section-6.6.1" + Environment.NewLine + "Errod ocurred on post action in product controller"
            };
        }
    }
}
