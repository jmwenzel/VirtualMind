using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace VirtualMind.WebAPI.App.Cqs
{
    using Infrastructure.Proxies;
    using Models;

    /// <summary>
    /// Create transaction command handler
    /// </summary>
    public class CreateTransactionAsyncCmdHandler : IRequestHandler<CreateTransactionAsyncCmd, ExchangeTransactionModel>
    {
        private readonly ICurrencyProxy _providerProxy;
        private readonly ITransactionProxy _transactionProxy;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="providerProxy"></param>
        /// <param name="transactionProxy"></param>
        public CreateTransactionAsyncCmdHandler(ICurrencyProxy providerProxy, ITransactionProxy transactionProxy)
        {
            _providerProxy = providerProxy ?? throw new ArgumentNullException(nameof(providerProxy));
            _transactionProxy = transactionProxy ?? throw new ArgumentNullException(nameof(transactionProxy));
        }

        /// <inheritdoc />
        public async Task<ExchangeTransactionModel> Handle(CreateTransactionAsyncCmd request, CancellationToken cancellationToken)
        {
            
            var input = request.TransactionInput;
            var currency = input.Currency.ToUpper().Trim();

            var exchange = await _providerProxy.GetExchangeForTransaction(currency);

            var totalTransactions = await _transactionProxy.GetTotalTransactions(input.UserId, currency);

            var transactionModel = await _transactionProxy.Insert(input, exchange.Exchange, totalTransactions, exchange.Threshold);

            return transactionModel;
            
        }
    }
}
