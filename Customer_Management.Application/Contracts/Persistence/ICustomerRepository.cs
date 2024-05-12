using Customer_Management.Application.DTOs.Customer;
using Customer_Management_Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Management.Application.Persistence.Contracts
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<bool> CustomerExists(Customer customer);
        Task<Customer> GetCustomerByEmail(string email);
        Task<bool> ExistEmail(string email);
        
    }
}
