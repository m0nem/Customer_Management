using Customer_Management.MVC.Models.Identity;

namespace Customer_Management.MVC.Contracts
{
    public interface IAuthenticateService
    {
        Task<bool> Authenticate(string email, string password);
        Task<bool> Register(RegisterVM register);
        Task Logout();
    }
}
