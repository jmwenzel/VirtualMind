namespace VirtualMind.WebAPI.App.Models
{
    /// <summary>
    /// Exchange transaction model
    /// </summary>
    public class ExchangeTransactionModel
    {
        /// <summary>
        /// User Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Amount  
        /// </summary>
        public decimal PesosAmount { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// Currency exchange value
        /// </summary>
        public decimal CurrencyExchange { get; set; }
        /// <summary>
        /// Amount exchanged
        /// </summary>
        public decimal ExchangedAmount { get; set; }
    }
}
