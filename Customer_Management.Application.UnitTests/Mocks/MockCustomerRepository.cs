using Customer_Management.Application.Persistence.Contracts;
using Customer_Management_Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Management.Application.UnitTests.Mocks
{
    public static class MockCustomerRepository
    {
        public static Mock<ICustomerRepository> GetCustomerRepository()
        {

            var customers = new List<Customer_Management_Domain.Customer>()
            {
                new Customer_Management_Domain.Customer() {
                    Id = 1,
                    BankAccountNumber = 1,
                    DateOfBirth = DateTime.Now,
                    Email="monem@gmial.com",
                    FirstName="monem",
                    LastName="Mousavi",
                    Phone = "+989396080826"
                }, new Customer_Management_Domain.Customer() {
                    Id = 2,
                    BankAccountNumber = 1,
                    DateOfBirth = DateTime.Now,
                    Email="alireza@gmial.com",
                    FirstName="reza",
                    LastName="rezaii",
                    Phone = "+989396080826"
                }, new Customer_Management_Domain.Customer() {
                    Id = 3,
                    BankAccountNumber = 1,
                    DateOfBirth = DateTime.Now,
                    Email="john@gmial.com",
                    FirstName="Smith",
                    LastName="heygel",
                    Phone = "+989396080826"
                }
            };

            var mockRepo = new Mock<ICustomerRepository>();
            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(customers);
            mockRepo.Setup(r => r.Add(It.IsAny<Customer_Management_Domain.Customer>()))
                .ReturnsAsync((Customer_Management_Domain.Customer customer) =>
                {
                    customers.Add(customer);
                    return customer;
                });

            return mockRepo;

        }
    }
}
