using Customer_Management.Application.DTOs.Customer;
using Customer_Management.Application.Persistence.Contracts;
using Customer_Management_Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Management.Persistence.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly MyDbContext _dbContext;

        public CustomerRepository(MyDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> CustomerExists(Customer customer)
        {
            return await _dbContext.Customers.CountAsync(
              c =>
               c.FirstName.Trim().ToLower().Equals(customer.FirstName.ToLower().Trim()) &&
               c.LastName.ToLower().Trim().Equals(customer.LastName.ToLower().Trim()) &&
               c.DateOfBirth == customer.DateOfBirth) > 0;
        }


        public async Task<Customer> GetCustomerByEmail(string email)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(c => c.Email.ToLower().Trim().Equals(email.ToLower().Trim()));
        }

        public async Task<bool> ExistEmail(string email)
        {
            return await _dbContext.Customers.CountAsync(c => c.Email.ToLower().Trim().Equals(email.ToLower().Trim())) > 0;
        }


    }
}
