using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace VirtualMind.WebAPI.Infrastructure.Data
{
    using Dapper;
    using DbEntities;
    using System.Linq;


    /// <summary>
    /// Transaction Repository
    /// </summary>
    public class TransactionRepository : BaseRepository<TransactionDbEntity>, ITransactionRepository
    {
        private readonly ILogger<TransactionRepository> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public TransactionRepository(IConfiguration configuration, ILogger<TransactionRepository> logger)
            :base(configuration, logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        public async Task<decimal> GetTotalTransactions(int userId, string currency)
        {
            try
            {
                var procedure = "[dbo].[GetTotalTransactions]";
                var parameters = new { UserId = userId, @Currency = currency.ToUpper().Trim() };
                decimal total = 0;

                using (var conn = _connection)
                {
                    var result = await conn.QueryAsync<decimal>(procedure, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    total = result.FirstOrDefault();
                }
                return total;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while executing the Sql command.");
                throw new NotSupportedException("Unable to retrieve data");
            }
        }
    }
}
