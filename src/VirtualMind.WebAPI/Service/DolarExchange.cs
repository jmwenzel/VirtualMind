using Flurl.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VirtualMind.WebAPI.Service
{
    using App.Models;
    using AutoMapper;
    using Infrastructure.Core;

    /// <summary>
    /// Dolar Exchange class
    /// </summary>
    public class DolarExchange : IExchange
    {
        private const string DOLAR_CURRENCY_CODE = "USD";
        private readonly ThresholdSettings _thresholdSettings;
        private readonly ExchangeProviderSettings _settings;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        public DolarExchange(IOptions<ExchangeProviderSettings> settings, 
            IOptions<ThresholdSettings> thresholdSettings, 
            IMapper mapper)
        {
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
            _thresholdSettings = thresholdSettings?.Value ?? throw new ArgumentNullException(nameof(thresholdSettings));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        /// <inheritdoc />
        public bool IsCurrency(string currency)
        {
            return currency.Equals(DOLAR_CURRENCY_CODE, StringComparison.OrdinalIgnoreCase);
        }

        /// <inheritdoc />
        public async Task<CurrencyExchangeModel> GetExchange()
        {
            return await GetExchangeModel();
        }

        /// <inheritdoc />
        public async Task<CurrencyExchangeModelTransaction> GetExchangeForTransaction()
        {
            try
            {
                var result = _mapper.Map<CurrencyExchangeModelTransaction>(await GetExchangeModel());
                result.Threshold = _thresholdSettings.USD;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        private async Task<CurrencyExchangeModel> GetExchangeModel()
        {
            var response = await _settings.Dolar.GetJsonAsync<IList<string>>();

            var result = new CurrencyExchangeModel
            {
                Currency = DOLAR_CURRENCY_CODE,
                Exchange = Convert.ToDecimal(response[0]),
                Date = response[2]
            };

            return result;
        }
    }
}
