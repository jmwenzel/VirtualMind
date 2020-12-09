using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace VirtualMind.WebAPI.App.Controllers
{
    using Models;

    /// <summary>
    /// Base controller
    /// </summary>
    public class BaseController : ControllerBase
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        public BaseController(ILogger<BaseController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        /// <summary>
        /// Response Wrapper
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        /// <param name="errorMessage"></param>
        /// <param name="successMessage"></param>
        /// <returns></returns>
        protected async Task<ApiResponse<T>> ExecuteAsync<T>
            (Func<Task<T>> method, string errorMessage = null, string successMessage = null)
        {
            var response = new ApiResponse<T>();

            try
            {
                var result = await method.Invoke();
                response.Data = result;
                if (result != null)
                {
                    response.AddSuccessMessage(successMessage);
                }
                else
                {
                    response.AddSuccessMessage("No results to display");
                }
            }
            catch (KeyNotFoundException ex)
            {
                this.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                HandlerErrorLog(ex, errorMessage, response);
            }
            catch (NotImplementedException ex)
            {
                this.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotImplemented;
                HandlerErrorLog(ex, errorMessage, response);
            }
            catch (NotSupportedException ex)
            {
                this.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotImplemented;
                HandlerErrorLog(ex, errorMessage, response);
            }
            catch (Exception ex)
            {
                this.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                HandlerErrorLog(ex, errorMessage, response);
            }

            return response;
        }

        private void HandlerErrorLog<T>(Exception ex,
            string defaultErrorMessage,
            ApiResponse<T> response)
        {
            if (string.IsNullOrEmpty(defaultErrorMessage))
                defaultErrorMessage = "An error occurred while processing the request.";

            if (ex is KeyNotFoundException || ex is FormatException || ex is NotImplementedException || ex is NotSupportedException)
                defaultErrorMessage = defaultErrorMessage + " " + ex.Message;

            response.AddErrorMessage($"{defaultErrorMessage}");
            _logger.LogError(defaultErrorMessage);
        }
    }
}
