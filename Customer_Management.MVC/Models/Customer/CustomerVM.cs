namespace Customer_Management.MVC.Models.Customer
{
    public class CustomerVM
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int BankAccountNumber { get; set; }
    }
}
