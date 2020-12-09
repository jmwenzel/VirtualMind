using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualMind.WebAPI.Infrastructure.Proxies
{
    using App.Models;
    using Service;

    /// <summary>
    /// Currency Provider proxy
    /// </summary>
    public class CurrencyProxy : ICurrencyProxy
    {
        private readonly IEnumerable<IExchange> _providers;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="providers"></param>
        public CurrencyProxy(IEnumerable<IExchange> providers)
            => _providers = providers ?? throw new ArgumentNullException(nameof(providers));

        /// <inheritdoc />
        public async Task<CurrencyExchangeModel> GetExchange(string currency)
        {
            var provider = GetExchangeProvider(currency);
            return await provider.GetExchange();
        }

        /// <inheritdoc />
        public async Task<CurrencyExchangeModelTransaction> GetExchangeForTransaction(string currency)
        {
            var provider = GetExchangeProvider(currency);
            return await provider.GetExchangeForTransaction();
        }

        private IExchange GetExchangeProvider(string currency)
        {
            var provider = _providers.Where(p => p.IsCurrency(currency)).FirstOrDefault();

            if (provider == null)
                throw new NotImplementedException("Currency is not available at the moment");

            return provider;
        }
    }
}
