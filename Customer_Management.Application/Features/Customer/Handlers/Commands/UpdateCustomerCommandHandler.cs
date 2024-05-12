using AutoMapper;
using Customer_Management.Application.DTOs.Customer.Validators;
using Customer_Management.Application.Exceptions;
using Customer_Management.Application.Features.Customer.Requests.Commands;
using Customer_Management.Application.Persistence.Contracts;
using Customer_Management.Application.Responses;
using Customer_Management_Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Customer_Management.Application.Features.Customer.Handlers.Commands
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, BaseCommandResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateCustomerValidator(_customerRepository);
            var validationResult = await validator.ValidateAsync(request.Customer);
            if (validationResult.IsValid == false)
            {
                //throw new ValidationException(validationResult);
                response.Success = false;
                response.Message = "Update Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }
            else
            {
                var customer = await _customerRepository.Get(request.Customer.Id);
                _mapper.Map(request.Customer, customer);
                await _customerRepository.Update(customer);
                response.Success = true;
                response.Errors = new List<string>() {" "," " };
                response.Message = "Update Successful";
                response.Id = customer.Id;
            }
            return response;
        }

      
    }
}
