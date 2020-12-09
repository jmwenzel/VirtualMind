using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace VirtualMind.WebAPI.App.Cqs
{
    /// <summary>
    /// Logging behavior class, implements the <see cref="IPipelineBehavior{TRequest, TResponse}"/>
    /// interface from MediatR
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private Stopwatch _chrono;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Handler method
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellation"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellation, RequestHandlerDelegate<TResponse> next)
        {
            _chrono = new Stopwatch();
            TResponse response;
            try
            {
                _logger.LogInformation($"Request for {request.GetType().Name} received");

                _chrono.Start();
                // Invoke the next one in the pipeline
                response = await next();
                //
                _chrono.Stop();
                _logger.LogInformation($"Request for {request.GetType().Name} completed in {_chrono.ElapsedMilliseconds} miliseconds");
                    
            }
            catch (Exception ex)
            {
                HandleException(request, ex);
                
                throw;
            }
            return response;
        }

        private void HandleException(TRequest request, Exception ex)
        {
            _chrono.Stop();
            _logger.LogError($"Request for {request.GetType().Name} failed in {_chrono.ElapsedMilliseconds} miliseconds");
        }
    }
}
