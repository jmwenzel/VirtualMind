using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace VirtualMind.WebAPI.App.Controllers
{
    using Cqs;
    using Models;
    using Infrastructure.Core;

    /// <summary>
    /// Currency controller
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route(Config.ROUTE_PREFFIX + "/v{version:apiVersion}/currency")]
    public class CurrencyController : BaseController
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger"></param> 
        public CurrencyController(IMediator mediator, ILogger<CurrencyController> logger) : base(logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Return currency exchange 
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        [HttpGet("{currency}", Name = "GetCurrencyExchange")]
        public async Task<ApiResponse<CurrencyExchangeModel>> GetCurrencyExchange(string currency)
        {
            return await ExecuteAsync(async () =>
            {
                return await _mediator.Send(new GetCurrencyExchangeQuery(currency));
            });
        }

        /// <summary>
        /// Creates a transaction
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        [HttpPost("transaction", Name = "ExchangeTransaction")]
        public async Task<ApiResponse<ExchangeTransactionModel>> ExchangeTransaction([FromBody] ExchangeTransactionInput transaction)
        {
            return await ExecuteAsync(async () =>
            {
                return await _mediator.Send(new CreateTransactionAsyncCmd(transaction));
            });
        }
    }
}