using BusinessLogic;
using DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicTests
{
    [TestFixture]
    public class CustomerManagerTests
    {
        private Customer customer1;
        private Customer customer2;
        private List<Customer> _availableCustomers;
        private CustomersManager _customersManagers;
        private Mock<IRepository> _customersManagersRepoMock;
        
        [SetUp]
        public void Setup()
        {
            customer1 = new Customer() { Id = 2, IdCard = "X1", Name = "N1", Surname = "S1" };

            customer2 = new Customer() { Id = 2, IdCard = "X2", Name = "N2", Surname = "S2" };

            _availableCustomers = new List<Customer> { customer1, customer2 };

            _customersManagersRepoMock = new Mock<IRepository>();

            _customersManagersRepoMock.Setup(x => x.GetAllCustomer()).Returns(_availableCustomers);

            _customersManagers = new CustomersManager(
                _customersManagersRepoMock.Object
                );

        }

        [Test]
        public void GetAllCustomers_InvokeMethod_CheckIfRepoIsCalled()
        {
            _customersManagers.GetAllCustomer();
            _customersManagersRepoMock.Verify(x => x.GetAllCustomer(), Times.Once);
        }
    }
}
