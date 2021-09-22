using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    using System.Linq;
    using DataTransferObjects;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Repository.IRepository" />
    public class InMemoryRepository : IRepository
    {
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// The customers
        /// </summary>
       // private readonly List<Customer> Customers;

        /// <summary>
        /// The balances
        /// </summary>
      //  private readonly Dictionary<Int32, Decimal> Balances;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryRepository"/> class.
        /// </summary>
        public InMemoryRepository(ApplicationDbContext db)
        {
            _db = db;
           // this.Customers = new List<Customer>();
           // this.Customers = _db.Customers.ToList();
          //  this.Balances = new Dictionary<Int32, Decimal>();
        }

        /// <summary>
        /// Deletes the customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteCustomer(Int32 id)
        {
            Customer customer = this.GetCustomer(id);
            this._db.Customers.Remove(customer);
        }

        /// <summary>
        /// Deposits the funds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="funds">The funds.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void DepositFunds(Int32 customerId,
                                 Decimal funds)
        {
            if (this._db.Balances.Any(b => b.CustomerId == customerId))
            {
                Balance balance = _db.Balances.First(i => i.CustomerId == customerId);
                balance.Funds += funds;
            }
            else
            {
                Balance balance = new Balance();
                balance.CustomerId = customerId;
                balance.Funds = funds;
                _db.Balances.Add(balance);
            }
            _db.SaveChanges();

            SaveTransaction("Credit", funds, customerId);

            //if (this.Balances.ContainsKey(customerId))
            //{
            //    this.Balances[customerId] += funds;
            //}
            //else
            //{
            //    this.Balances.Add(customerId, funds);
            //}
        }

        public IEnumerable<Customer> GetAllCustomer()
        {
            return this._db.Customers;
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the available funds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        public Decimal GetAvailableFunds(Int32 customerId)
        {
            if (this._db.Balances.Any(b => b.CustomerId == customerId))
            {
                return this._db.Balances.Where(i => i.CustomerId == customerId)
                                    .Select(i => i.Funds)
                                    .SingleOrDefault();
            }
            else
            {
                return 0;
            }
                      
            //return this.Balances.ContainsKey(customerId) ? this.Balances[customerId] : 0;
        }

        /// <summary>
        /// Gets the customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Customer GetCustomer(Int32 id)
        {
           
            return _db.Customers.FirstOrDefault(c => c.Id == id);
        }

        /// <summary>
        /// Saves the customer.
        /// </summary>
        /// <param name="customer">The customer.</param>
        public void SaveCustomer(Customer customer)
        {
            if (this.DoesCustomerExist(customer.Id))
            {
                Customer existingCustomer = this.GetCustomer(customer.Id);
                existingCustomer.IdCard = customer.IdCard;
                existingCustomer.Name = customer.Name;
                existingCustomer.Surname = customer.Surname;
               
            }
            else
            {
                this._db.Customers.Add(customer);
            }
            //new code
            _db.SaveChanges();
        }

        /// <summary>
        /// Withdraws the funds.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <param name="funds">The funds.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void WithdrawFunds(Int32 customerId,
                                  Decimal funds)
        {
            if(this._db.Balances.Any(b=>b.CustomerId == customerId))
            {
                Balance balance = _db.Balances.First(i => i.CustomerId == customerId);

                if (balance.Funds > funds)
                {
                    balance.Funds -= funds;
                    _db.SaveChanges();
                    SaveTransaction("Debit", funds, customerId);
                }

               

            }

            //if (this.Balances.ContainsKey(customerId))
            //{
            //    this.Balances[customerId] -= funds;
            //}
        }

        /// <summary>
        /// Does the customer exist.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        private Boolean DoesCustomerExist(Int32 id)
        {
            return this._db.Customers.Any(c => c.Id == id);
        }

        public void TransferFunds(Int32 customerIdFrom, Int32 customerIdTo, Decimal funds)
        {
            Decimal customerIdFromBalance = GetAvailableFunds(customerIdFrom);

            if(customerIdFromBalance > funds)
            {
                WithdrawFunds(customerIdFrom, funds);

                DepositFunds(customerIdTo, funds);
            }
        }

        public void SaveTransaction(String status, Decimal amount, Int32 createdBy)
        {
            Transaction transaction = new Transaction();
            transaction.Status = status;
            transaction.CreatedBy = createdBy;
            transaction.CreatedOn = Convert.ToDateTime(DateTime.Now.ToString());
            if (status == "Credit")
            {
                transaction.Credit = amount;
                transaction.Debit = null;
            }
            else
            {
                transaction.Debit = amount;
                transaction.Credit = null;
            }
            _db.Transactions.Add(transaction);
            _db.SaveChanges();
        }
    }
}
