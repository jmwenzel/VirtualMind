using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualMind.WebAPI.Infrastructure.Core
{
    /// <summary>
    /// Custom exception filter
    /// No need to do try-catch on each method
    /// </summary>
    public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Override from <see cref="ExceptionFilterAttribute"/>.
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            var exceptionMessage = "Internal Error";
#if DEBUG
            // More details in debug mode
            exceptionMessage = context.Exception.Message;
#endif
            
            var res = new ObjectResult(new
            {
                error = new
                {
                    Message = exceptionMessage
                }
            })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.Result = res;
        }
    }
}
