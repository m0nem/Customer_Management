using AutoMapper;
using Customer_Management.Application.DTOs.Customer;
using Customer_Management.Application.Features.Customer.Requests.Queries;
using Customer_Management.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Customer_Management.Application.Features.Customer.Handlers.Queries
{
    public class GetCustomerDetailRequestHandler : IRequestHandler<
        GetCustomerDetailRequest, CustomerDto
        >
    {
        public IMapper _mapper { get; }
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerDetailRequestHandler(ICustomerRepository customerRepository,IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }


        public async Task<CustomerDto> Handle(GetCustomerDetailRequest request, CancellationToken cancellationToken)
        {
            var customer=await _customerRepository.Get(request.Id);
            return _mapper.Map<CustomerDto>(customer);
        }
    }
}
