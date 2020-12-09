using System.Threading.Tasks;

namespace VirtualMind.WebAPI.Infrastructure.Proxies
{
    using App.Models;
    /// <summary>
    /// Currency provider interface
    /// </summary>
    public interface ICurrencyProxy
    {
        /// <summary>
        /// GetExchange
        /// </summary>
        /// <returns></returns>
        Task<CurrencyExchangeModel> GetExchange(string currency);

        /// <summary>
        /// Get currency for Transaction
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        Task<CurrencyExchangeModelTransaction> GetExchangeForTransaction(string currency);
    }
}
