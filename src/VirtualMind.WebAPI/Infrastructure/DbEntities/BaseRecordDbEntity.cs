using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualMind.WebAPI.Infrastructure
{
    /// <summary>
    /// Base entity record
    /// </summary>
    public class BaseRecordDbEntity
    {
        /// <summary>
        /// Created At
        /// </summary>
        [Column("CreateDate")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Updated At
        /// </summary>
        [Column("UpdateDate")]
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// Is record active
        /// </summary>
        [Column("IsActive")]
        public bool IsActive { get; set; }
    }
}
