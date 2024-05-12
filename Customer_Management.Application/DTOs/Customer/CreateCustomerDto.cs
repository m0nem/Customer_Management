using Customer_Management.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer_Management.Application.DTOs.Customer
{
    public class CreateCustomerDto : BaseDto,ICustomerDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int BankAccountNumber { get; set; }
    }
}
 