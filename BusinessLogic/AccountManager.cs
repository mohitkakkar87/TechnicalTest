using System;
using System.Collections.Generic;
using DataTransferObjects;
using Repository;


namespace BusinessLogic
{
    public class AccountManager : IAccountManager
    {
        #region Fields

        /// <summary>
        /// The repository
        /// </summary>
        private readonly IRepository Repository;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public AccountManager(IRepository repository)
        {
            this.Repository = repository;
        }
        #endregion

        /// <summary>
        /// Deposit Funds to the specified Customer Id.
        /// </summary>
        /// <param name="customerId">The customerId.</param>
        /// <param name="funds">The funds.</param>
        public void DepositFunds(int customerId, decimal funds)
        {
            this.Repository.DepositFunds(customerId, funds);
           // throw new NotImplementedException();
        }

        /// <summary>
        /// Withdraw Funds to the specified Customer Id.
        /// </summary>
        /// <param name="customerId">The customerId.</param>
        /// <param name="funds">The funds.</param>
        public void WithdrawFunds(int customerId, decimal funds)
        {
            this.Repository.WithdrawFunds(customerId, funds);
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Get available funds from the specified Customer Id.
        /// </summary>
        /// <param name="customerId">The customerId.</param>
        public Decimal GetAvailableFunds(Int32 customerId)
        {
            return this.Repository.GetAvailableFunds(customerId);
        }

        /// <summary>
        /// Transfer funds from one customer to another
        /// </summary>
        /// <param name="customerIdFrom">The customerIdFrom.</param>
        /// <param name="customerIdTo">The customerIdTo.</param>
        /// <param name="funds">The funds.</param>
        public void TransferFunds(Int32 customerIdFrom, Int32 customerIdTo, Decimal funds)
        {
            this.Repository.TransferFunds(customerIdFrom, customerIdTo, funds);
        }
        #endregion
    }
}
