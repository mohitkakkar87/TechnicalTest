using DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryTests
{
    [TestFixture]
    public class InMemoryRepositoryTests
    {
        private Customer customer1;
        private Customer customer2;
        private Balance balance1;

        private DbContextOptions<ApplicationDbContext> options;

        public InMemoryRepositoryTests()
        {
            customer1 = new Customer() { Id = 2, IdCard = "X1", Name = "N1", Surname = "S1" };

            customer2 = new Customer() { Id = 2, IdCard = "X2", Name = "N2", Surname = "S2" };

            balance1 = new Balance() { Id = 0, CustomerId = 1, Funds = 50 };

        }

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "temp_GainingChanger").Options;
        }

        [Test]
        [Order(1)]
        [TestCase(true)]
        [TestCase(false)]
        public void SaveCustomer_Customer1_CheckTheValuesFromDatabase(bool customerExist)
        {
            //arrange

            //bool customerExist = true;
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new InMemoryRepository(context);
                repository.SaveCustomer(customer1);
            }


            //act
            if (customerExist)
            {
                using (var context = new ApplicationDbContext(options))
                {
                    var repository = new InMemoryRepository(context);

                    customer1.IdCard = "New Id Card";
                    customer1.Name = "New Name";
                    customer1.Surname = "New SurName";

                    repository.SaveCustomer(customer1);

                }

            }
            //assert
            using (var context = new ApplicationDbContext(options))
            {
                var customerFromDb = context.Customers.FirstOrDefault(u => u.Id == 1);

                Assert.AreEqual(customer1.Id, customerFromDb.Id);
                Assert.AreEqual(customer1.IdCard, customerFromDb.IdCard);
                Assert.AreEqual(customer1.Name, customerFromDb.Name);
                Assert.AreEqual(customer1.Surname, customerFromDb.Surname);

            }

        }

        [Test]
        [Order(2)]
        public void GetAllCustomers_Customer1and2_CheckBothTheCustomersFromDatabase()
        {
            //arrange
            var expectedResult = new List<Customer> { customer1, customer2 };



            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                var repository = new InMemoryRepository(context);
                repository.SaveCustomer(customer1);
                repository.SaveCustomer(customer2);
            }

            //act
            List<Customer> actualList;
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new InMemoryRepository(context);
                actualList = repository.GetAllCustomer().ToList();
            }


            //assert
            CollectionAssert.AreEqual(expectedResult, actualList, new CustomerCompare());
        }

        [Test]
        [Order(3)]
        public void GetCustomerById_Customer1_CheckCustomerFromDatabase()
        {
            //arrange
            var expectedResult = new Customer() { Id = 1, IdCard = "X1", Name = "N1", Surname = "S1" };



            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                var repository = new InMemoryRepository(context);
                repository.SaveCustomer(customer1);

            }

            //act
            Customer customerFromDB;
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new InMemoryRepository(context);
                customerFromDB = repository.GetCustomer(customer1.Id);
            }


            //assert
            Assert.AreEqual(expectedResult.Id, customerFromDB.Id);

        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public bool CheckIfCustomerExist_Customer1_CheckCustomerFromDatabase(int customerId)
        {
            //arrange


            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                var repository = new InMemoryRepository(context);
                repository.SaveCustomer(customer1);
                repository.SaveCustomer(customer2);

            }

            //assert
            using (var context = new ApplicationDbContext(options))
            {
                var customerFromDb = context.Customers.FirstOrDefault(u => u.Id == customerId);

                if (customerFromDb.Id > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void GetAvailableFunds_ForCustomer1_CheckBalanceFromDB(int customerId)
        {
            //arrange
            decimal expectedFunds;
            //assert
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new InMemoryRepository(context);
                repository.DepositFunds(customerId, balance1.Funds);

                expectedFunds = repository.GetAvailableFunds(customerId);
            }
            //act
            using (var context = new ApplicationDbContext(options))
            {
                var dbFunds = context.Balances.FirstOrDefault(u => u.CustomerId == customerId).Funds;
                Assert.AreEqual(expectedFunds, dbFunds);
            }

        }

       


        private class CustomerCompare : IComparer
        {
            public int Compare(object x, object y)
            {
                var c1 = (Customer)x;
                var c2 = (Customer)y;
                if (c1.Id != c2.Id)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
