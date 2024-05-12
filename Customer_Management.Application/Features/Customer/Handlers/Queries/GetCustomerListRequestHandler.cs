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
    public class GetCustomerListRequestHandler :
        IRequestHandler<GetCustomerListRequest, List<CustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        public IMapper _mapper { get; }

        public GetCustomerListRequestHandler(ICustomerRepository customerRepository
            ,IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }


        public async Task<List<CustomerDto>> Handle(GetCustomerListRequest request, CancellationToken cancellationToken)
        {
            var customerList= await _customerRepository.GetAll();
            return _mapper.Map<List<CustomerDto>>(customerList);
        }
    }
}
