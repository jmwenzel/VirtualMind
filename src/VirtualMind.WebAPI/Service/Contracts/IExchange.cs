using System.Threading.Tasks;

namespace VirtualMind.WebAPI.Service
{
    using App.Models;

    /// <summary>
    /// Exchange interface
    /// </summary>
    public interface IExchange
    {
        /// <summary>
        /// Verifies if currency input is implemented
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        bool IsCurrency(string currency);
        /// <summary>
        /// Get currency exchange
        /// </summary>
        /// <returns></returns>
        Task<CurrencyExchangeModel> GetExchange();
        /// <summary>
        /// Get currency exchange model for transaction
        /// </summary>
        /// <returns></returns>
        Task<CurrencyExchangeModelTransaction> GetExchangeForTransaction();
    }
}
