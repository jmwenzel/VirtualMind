using System.Threading.Tasks;

namespace VirtualMind.WebAPI.Infrastructure.Proxies
{
    using App.Models;
    /// <summary>
    /// Transaction Proxy
    /// </summary>
    public interface ITransactionProxy
    {
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="input"></param>
        /// <param name="exchange"></param>
        /// <param name="totalTransactions"></param>
        /// <param name="threshold"></param>
        /// <returns></returns>
        Task<ExchangeTransactionModel> Insert(ExchangeTransactionInput input, decimal exchange, decimal totalTransactions, decimal threshold);
        /// <summary>
        /// Get total transaction by user, currency
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        Task<decimal> GetTotalTransactions(int userId, string currency);
    }
}
