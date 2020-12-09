namespace VirtualMind.WebAPI.App.Models
{
    /// <summary>
    /// Exchange Transaction Input
    /// </summary>
    public class ExchangeTransactionInput
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
    }
}
