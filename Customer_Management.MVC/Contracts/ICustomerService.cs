using Customer_Management.MVC.Models.Customer;
using Customer_Management.MVC.Services.Base;

namespace Customer_Management.MVC.Contracts
{
    public interface ICustomerService
    {

        Task<List<CustomerVM>> GetCustomers();
        Task<CustomerVM> GetCustomerDetails(int id);
        Task<Response<int>> CreateCustomer(CreateCustomerVM customer);
        Task<Response<int>> UpdateCustomer(int id,CustomerVM customer);
        Task<Response<int>> DeleteCustomer(int id);
    }
}
