using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer_Management.Application.Features.Customer.Requests.Commands
{
    public class DeleteCustomerCommand:IRequest
    {
        public int Id { get; set; }
    }
}
