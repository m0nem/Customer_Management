using AutoMapper;
using Customer_Management.Application.Exceptions;
using Customer_Management.Application.Features.Customer.Requests.Commands;
using Customer_Management.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Customer_Management.Application.Features.Customer.Handlers.Commands
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository,IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.Get(request.Id);
            if (customer == null)
                throw new NotFoundException(nameof(customer), request.Id);

            await _customerRepository.Delete(customer);
            return Unit.Value;
        }
    }
}
