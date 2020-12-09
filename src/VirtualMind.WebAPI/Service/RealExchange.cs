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
    /// Real Exchange class
    /// </summary>
    public class RealExchange : IExchange
    {
        private const string REAL_CURRENCY_CODE = "BRL";

        private readonly ExchangeProviderSettings _settings;
        private readonly ThresholdSettings _thresholdSettings;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        public RealExchange(IOptions<ExchangeProviderSettings> settings,
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
            return currency.Equals(REAL_CURRENCY_CODE, StringComparison.OrdinalIgnoreCase);
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
                result.Threshold = _thresholdSettings.BRL;

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
                Currency = REAL_CURRENCY_CODE,
                Exchange = Convert.ToDecimal(response[0]) / 4,
                Date = response[2]
            };

            return result;
        }
    }
}
