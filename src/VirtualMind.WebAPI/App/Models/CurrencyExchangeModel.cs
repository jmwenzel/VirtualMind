namespace VirtualMind.WebAPI.App.Models
{
    /// <summary>
    /// Currency Exchange Model
    /// </summary>
    public class CurrencyExchangeModel
    {
        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// Exchange value
        /// </summary>
        public decimal Exchange { get; set; }
        /// <summary>
        /// Exchange date
        /// </summary>
        public string Date { get; set; }
    }
}
