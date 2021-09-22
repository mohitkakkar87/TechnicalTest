using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjects
{
    public class Transfer
    {
        /// <summary>
        /// Gets or sets the From Customer Id.
        /// </summary>
        /// <value>
        /// The From.
        /// </value>
        public Int32 From { get; set; }

        /// <summary>
        /// Gets or sets the To Customer Id.
        /// </summary>
        /// <value>
        /// The To.
        /// </value>
        public Int32 To { get; set; }

        /// <summary>
        /// Gets or sets the Funds.
        /// </summary>
        /// <value>
        /// The Funds.
        /// </value>
        public Decimal Funds { get; set; }
    }
}
