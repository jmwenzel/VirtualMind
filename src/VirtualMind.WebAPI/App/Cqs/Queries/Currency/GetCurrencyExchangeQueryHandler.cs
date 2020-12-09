using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace VirtualMind.WebAPI.App.Cqs
{
    using App.Models;
    using Infrastructure.Proxies;

    /// <summary>
    /// Currency Exchange Handler
    /// </summary>
    public class GetCurrencyExchangeQueryHandler : IRequestHandler<GetCurrencyExchangeQuery, CurrencyExchangeModel>
    {
        private readonly ICurrencyProxy _providerProxy;

        /// <summary>
        /// Constructor
        /// </summary>
        public GetCurrencyExchangeQueryHandler(ICurrencyProxy providerProxy)
        {
            _providerProxy = providerProxy ?? throw new ArgumentNullException(nameof(providerProxy));
        }

        /// <inheritdoc />
        public async Task<CurrencyExchangeModel> Handle(GetCurrencyExchangeQuery request, CancellationToken cancellation)
        {
            var exchange = await _providerProxy.GetExchange(request.Currency.Trim());

            return exchange;
            
        }
    }
}
