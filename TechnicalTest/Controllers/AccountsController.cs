using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTransferObjects;

namespace TechnicalTest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        #region Fields

        /// <summary>
        /// The Account manager
        /// </summary>
        //private readonly ICustomersManager CustomersManager;
        private readonly IAccountManager AccountManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomersController" /> class.
        /// </summary>
        /// <param name="customersManager">The customers manager.</param>
        public AccountsController(IAccountManager accountManager)
        {
            this.AccountManager = accountManager;
        }

        #endregion

        #region Methods


        

        // POST api/Withdraw/5
        [HttpPost("{customerId}")]
        public void Withdraw(Int32 customerId,[FromBody] Balance balance)
        {
           
            this.AccountManager.WithdrawFunds(customerId, balance.Funds);
        }

        // POST api/Deposit/5
        [HttpPost("{customerId}")]
        public void Deposit(Int32 customerId, [FromBody] Balance balance )
        {
           
            this.AccountManager.DepositFunds(customerId, balance.Funds);
        }

        // Get api/GetAvailableFunds/1
        [HttpGet("{id}")]
        public Decimal GetAvailableFunds(Int32 id)
        {
            return this.AccountManager.GetAvailableFunds(id);
        }

        // POST api/Transfer
        [HttpPost]
        public void Transfer([FromBody] Transfer transfer)
        {
            
            this.AccountManager.TransferFunds(transfer.From, transfer.To, transfer.Funds);

        }
        #endregion
    }
}
