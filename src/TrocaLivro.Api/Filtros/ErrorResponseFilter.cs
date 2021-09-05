using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TrocaLivro.Api.Filtros
{
    public class ErrorResponseFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var errorResponse = ErrorResponse.From(context.Exception);
            context.Result = new ObjectResult(errorResponse) { StatusCode = 500 };
        }
    }
}
