using API_DesignPatterns.Core;
using API_DesignPatterns.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API_DesignPatterns.API.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is DuplicatedAuthorException ||
                context.Exception is EntityAlreadyMarkedAsDeletedException)
            {
                context.Result = new ConflictObjectResult(new
                {
                    StatusCode = StatusCodes.Conflict,
                    context.Exception.Message
                });
            }
            else if (context.Exception is EntityNotFoundException)
            {
                context.Result = new NotFoundObjectResult(new
                {
                    StatusCode = StatusCodes.NotFound,
                    context.Exception.Message
                });
            }
        }
    }
}
