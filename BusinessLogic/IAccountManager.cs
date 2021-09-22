using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public interface IAccountManager
    {
        #region Methods

        /// <summary>
        /// Deposit Funds to the specified Customer Id.
        /// </summary>
        /// <param name="customerId">The customerId.</param>
        /// <param name="funds">The funds.</param>
        void DepositFunds(Int32 customerId,
                           Decimal funds);

        /// <summary>
        /// Withdraw Funds to the specified Customer Id.
        /// </summary>
        /// <param name="customerId">The customerId.</param>
        /// <param name="funds">The funds.</param>
        void WithdrawFunds(Int32 customerId,
                          Decimal funds);

        /// <summary>
        /// Get available funds from the specified Customer Id.
        /// </summary>
        /// <param name="customerId">The customerId.</param>
        Decimal GetAvailableFunds(Int32 customerId);

        /// <summary>
        /// Transfer funds from one customer to another
        /// </summary>
        /// <param name="customerIdFrom">The customerIdFrom.</param>
        /// <param name="customerIdTo">The customerIdTo.</param>
        /// <param name="funds">The funds.</param>
        void TransferFunds(Int32 customerIdFrom, Int32 customerIdTo, Decimal funds);
        #endregion
    }
}
