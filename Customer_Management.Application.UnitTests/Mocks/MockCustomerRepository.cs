using Customer_Management.Application.Persistence.Contracts;
using Customer_Management_Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain = Customer_Management_Domain;

namespace Customer_Management.Application.UnitTests.Mocks
{
    public static class MockCustomerRepository
    {
        
        public static Mock<ICustomerRepository> GetCustomerRepository()
        {
            var customers = new List<Domain.Customer>()
        {
            new Domain.Customer { Id = 1, BankAccountNumber = 123456790, DateOfBirth = DateTime.Now, Email = "monem@gmail.com", FirstName = "monem", LastName = "Mousavi", Phone = "+989396080826" },
            new Domain.Customer { Id = 2, BankAccountNumber = 123456790, DateOfBirth = DateTime.Now, Email = "alireza@gmail.com", FirstName = "reza", LastName = "rezaii", Phone = "+989396080826" },
            new Domain.Customer { Id = 3, BankAccountNumber = 123456790, DateOfBirth = DateTime.Now, Email = "john@gmail.com", FirstName = "Smith", LastName = "heygel", Phone = "+989396080826" }
        };


            var mockRepo = new Mock<ICustomerRepository>();

            // Get all customers
            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(customers);

            // Get a customer by id
            mockRepo.Setup(r => r.Get(It.IsAny<int>()))
                .ReturnsAsync((int id) => customers.FirstOrDefault(c => c.Id == id));

            // Check if a customer exists
            mockRepo.Setup(r => r.Exist(It.IsAny<int>()))
                .ReturnsAsync((int id) => customers.Any(c => c.Id == id));

            // Add a customer
            mockRepo.Setup(r => r.Add(It.IsAny<Domain.Customer>()))
                .ReturnsAsync((Domain.Customer customer) =>
                {
                    customers.Add(customer);
                    return customer;
                });

            // Update a customer
            mockRepo.Setup(r => r.Update(It.IsAny<Domain.Customer>()))
                .Callback((Domain.Customer customer) =>
                {
                    var existingCustomer = customers.FirstOrDefault(c => c.Id == customer.Id);
                    if (existingCustomer != null)
                    {
                        customers.Remove(existingCustomer);
                        customers.Add(customer);
                    }
                });

            // Delete a customer
            mockRepo.Setup(r => r.Delete(It.IsAny<Domain.Customer>()))
                .Callback((Domain.Customer customer) =>
                {
                    var customerToDelete = customers.FirstOrDefault(c => c.Id == customer.Id);
                    if (customerToDelete != null)
                    {
                        customers.Remove(customerToDelete);
                    }
                });

            return mockRepo;
        }
    }
}
