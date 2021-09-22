using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataTransferObjects
{
    public class Transaction
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }

        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        /// <value>
        /// The Status.
        /// </value>
        [StringLength(50)]
        public String Status { get; set; }

        /// <summary>
        /// Gets or sets the Credited Value.
        /// </summary>
        /// <value>
        /// The Credit.
        /// </value>
        [Column(TypeName = "decimal(18,2)")]
        public Decimal? Credit { get; set; }


        /// <summary>
        /// Gets or sets the Debited Value.
        /// </summary>
        /// <value>
        /// The Debit.
        /// </value>
        [Column(TypeName = "decimal(18,2)")]
        public Decimal? Debit { get; set; }

        /// <summary>
        /// Gets or sets the Created By.
        /// </summary>
        /// <value>
        /// The CreatedBy.
        /// </value>
        public Int32 CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the Created On.
        /// </summary>
        /// <value>
        /// The CreatedOn.
        /// </value>
        public DateTime CreatedOn { get; set; }
    }
}
