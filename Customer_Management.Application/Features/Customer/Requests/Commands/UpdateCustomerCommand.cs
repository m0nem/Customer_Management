using Customer_Management.Application.DTOs.Customer;
using Customer_Management.Application.Responses;
using MediatR;

namespace Customer_Management.Application.Features.Customer.Requests.Commands
{
    public class UpdateCustomerCommand :IRequest<BaseCommandResponse>
    {
        public UpdateCustomerDto Customer { get; set; }

    }
}
