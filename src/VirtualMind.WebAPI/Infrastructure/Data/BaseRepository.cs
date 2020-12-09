using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace VirtualMind.WebAPI.Infrastructure.Data
{
    /// <summary>
    /// Base repository class to be inherited
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T> : IRepository<T> where T : BaseRecordDbEntity
    {
        /// <summary>
        /// Configuration
        /// </summary>
        protected IConfiguration _configuration;

        private ILogger _logger;

        /// <summary>
        /// Db Connection
        /// </summary>
        protected DbConnection _connection
        {
            get
            {
                var conn = _configuration.GetConnectionString("CurrencyExchange");
                
                return new SqlConnection(conn);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param> 
        protected BaseRepository(IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        public async Task<int?> Insert(T model)
        {
            return await ExecuteQuery(async conn =>
            {
                return await conn.InsertAsync(model);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TX"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        protected async Task<TX> ExecuteQuery<TX>(Func<DbConnection, Task<TX>> query)
        {
            try
            {
                using (var conn = _connection)
                {
                    conn.Open();
                    return await query.Invoke(conn);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while executing the Sql command.");
                throw ex;
            }
        }
    }
}
