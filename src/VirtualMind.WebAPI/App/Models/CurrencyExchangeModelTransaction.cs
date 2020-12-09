namespace VirtualMind.WebAPI.App.Models
{
    /// <summary>
    /// Currency Exchange model for transaction
    /// </summary>
    public class CurrencyExchangeModelTransaction : CurrencyExchangeModel
    {
        /// <summary>
        /// Threshold
        /// </summary>
        public decimal Threshold { get; set; }
    }
}
