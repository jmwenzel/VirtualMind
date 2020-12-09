using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualMind.WebAPI.App.Models;
using VirtualMind.WebAPI.Infrastructure.DbEntities;

namespace VirtualMind.WebAPI.App.Mappings
{
    /// <summary>
    /// Transaction maps profile
    /// </summary>
    public class TransactionMaps : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TransactionMaps()
        {
            CreateMap<ExchangeTransactionInput, ExchangeTransactionModel>();

            CreateMap<ExchangeTransactionModel, TransactionDbEntity>();

            CreateMap<CurrencyExchangeModel, CurrencyExchangeModelTransaction>();
        }
    }
}
