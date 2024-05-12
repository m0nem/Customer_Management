using Customer_Management.Application.DTOs.Customer;
using Customer_Management.Application.Responses;
using MediatR;

namespace Customer_Management.Application.Features.Customer.Requests.Commands
{
    public class CreateCustomerCommand:IRequest<BaseCommandResponse>
    {
        public CreateCustomerDto CustomerDto { get; set; }
    }
}
