using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualMind.WebAPI.Infrastructure.DbEntities
{
    /// <summary>
    /// Transaction Db Entity
    /// </summary>
    [Table("Transaction")]
    public class TransactionDbEntity : BaseRecordDbEntity
    {
        /// <summary>
        /// Transaction Id
        /// </summary>
        [Key]
        [Column("ID")]
        public int ID { get; set; }
        /// <summary>
        /// User Id
        /// </summary>
        [Required]
        [Column("UserId")]
        public int UserId { get; set; }
        /// <summary>
        /// PesosAmount
        /// </summary>
        [Required]
        [Column("PesosAmount")]
        public decimal PesosAmount { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        [Required]
        [Column("Currency")]
        public string Currency { get; set; }
        /// <summary>
        /// Currency exchange value
        /// </summary>
        [Required]
        [Column("CurrencyExchange")]
        public decimal CurrencyExchange { get; set; }
        /// <summary>
        /// Amount exchanged
        /// </summary>
        [Required]
        [Column("ExchangedAmount")]
        public decimal ExchangedAmount { get; set; }
    }
}
