using MediatR;

namespace VirtualMind.WebAPI.App.Cqs
{
    using Models;

    /// <summary>
    /// Currency Exchange Query
    /// </summary>
    public class GetCurrencyExchangeQuery : IRequest<CurrencyExchangeModel>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="currency"></param>
        public GetCurrencyExchangeQuery(string currency)
            => Currency = currency.ToUpper().Trim();
        

        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; }
    }
}
