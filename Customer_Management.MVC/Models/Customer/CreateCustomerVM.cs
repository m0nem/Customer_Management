namespace Customer_Management.MVC.Models.Customer
{
    public class CreateCustomerVM
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public System.DateTimeOffset DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int BankAccountNumber { get; set; }
    }
}
