using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualMind.WebAPI.Infrastructure.DbEntities;

namespace VirtualMind.WebAPI.Infrastructure.Data
{
    /// <summary>
    /// Transaction repository interface
    /// </summary>
    public interface ITransactionRepository : IRepository<TransactionDbEntity>
    {
        /// <summary>
        /// Get total transaction by user, currency
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        Task<decimal> GetTotalTransactions(int userId, string currency);
    }
}
