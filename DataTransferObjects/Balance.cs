using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataTransferObjects
{
    public class Balance
    {
        #region Fields

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }

        /// <summary>
        /// Gets or sets the CustomerId.
        /// </summary>
        /// <value>
        /// The CustomerId.
        /// </value>
        public Int32 CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the Funds.
        /// </summary>
        /// <value>
        /// The surname.
        /// </value>
        [Column(TypeName = "decimal(18,2)")]
        public Decimal Funds { get; set; }

       

        #endregion
    }
}
