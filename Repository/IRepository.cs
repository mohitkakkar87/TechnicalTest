namespace Repository
{
    using System;
    using System.Collections.Generic;
    using DataTransferObjects;

    public interface IRepository
    {
        #region Methods

        /// <summary>
        /// Deletes the customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteCustomer(Int32 id);

        /// <summary>
        /// Deposits the funds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="funds">The funds.</param>
        void DepositFunds(Int32 customerId,
                          Decimal funds);

        /// <summary>
        /// Gets the available funds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        Decimal GetAvailableFunds(Int32 customerId);

        /// <summary>
        /// Gets the customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Customer GetCustomer(Int32 id);

        /// <summary>
        /// Saves the customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        void SaveCustomer(Customer customer);

        /// <summary>
        /// Withdraws the funds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="funds">The funds.</param>
        void WithdrawFunds(Int32 customerId,
                           Decimal funds);

        /// <summary>
        /// Get All Customers
        /// </summary>
        
        IEnumerable<Customer> GetAllCustomer();

        /// <summary>
        /// Transfer funds .
        /// </summary>
        /// <param name="customerIdFrom">The customer identifier.</param>
        /// <param name="customerIdTo">The funds.</param>
        /// <param name="funds">The funds.</param>
        void TransferFunds(Int32 customerIdFrom, Int32 customerIdTo, Decimal funds);

        /// <summary>
        /// Save Transactions to generate history.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="createdBy">The createdBy.</param>
        void SaveTransaction(String status, Decimal amount, Int32 createdBy);
        #endregion
    }
}