using AutoMapper;
using System;
using System.Threading.Tasks;

namespace VirtualMind.WebAPI.Infrastructure.Proxies
{
    using App.Models;
    using Infrastructure.Data;
    using Infrastructure.DbEntities;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Transaction Proxy class
    /// </summary>
    public class TransactionProxy : ITransactionProxy
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogger<TransactionProxy> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        public TransactionProxy(IMapper mapper, ITransactionRepository transactionRepository,
            ILogger<TransactionProxy> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        public async Task<ExchangeTransactionModel> Insert(ExchangeTransactionInput input, decimal exchange, 
            decimal totalTransactions, decimal threshold)
        {
            try
            {
                var transactionModel = _mapper.Map<ExchangeTransactionModel>(input);
                transactionModel.ExchangedAmount = transactionModel.PesosAmount / exchange;
                transactionModel.CurrencyExchange = exchange;

                if (totalTransactions + transactionModel.ExchangedAmount > threshold)
                    throw new NotSupportedException("Amount is over the limit");

                var transactionEntity = _mapper.Map<TransactionDbEntity>(transactionModel);
                transactionEntity.CreatedAt = DateTime.Now;
                transactionEntity.UpdatedAt = DateTime.Now;
                var id = await _transactionRepository.Insert(transactionEntity);

                if (id > 0)
                    return transactionModel;
                else
                    throw new NotSupportedException("There was an error storing data");
            }
            catch (NotSupportedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
            
        }

        /// <inheritdoc />
        public async Task<decimal> GetTotalTransactions(int userId, string currency)
        {
            try
            {
                return await _transactionRepository.GetTotalTransactions(userId, currency);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
