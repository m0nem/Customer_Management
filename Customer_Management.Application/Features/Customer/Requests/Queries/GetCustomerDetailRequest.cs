using Customer_Management.Application.DTOs.Customer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer_Management.Application.Features.Customer.Requests.Queries
{
    public class GetCustomerDetailRequest : IRequest<CustomerDto>
    {
        public int Id { get; set; }
        //public CustomerDto Customer { get; set; }
    }
}
