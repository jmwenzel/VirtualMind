using MediatR;
using System;

namespace VirtualMind.WebAPI.App.Cqs
{
    using Models;

    /// <summary>
    /// Create transaction command
    /// </summary>
    public class CreateTransactionAsyncCmd : IRequest<ExchangeTransactionModel>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="request"></param>
        public CreateTransactionAsyncCmd(ExchangeTransactionInput request)
            => TransactionInput = request ?? throw new ArgumentNullException(nameof(request));

        /// <summary>
        /// Transaction Input
        /// </summary>
        public ExchangeTransactionInput TransactionInput { get; }
    }
}
