using AutoMapper;
using Customer_Management.Application.DTOs.Customer;
using Customer_Management.Application.Features.Customer.Requests.Commands;
using Customer_Management.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Customer_Management_Domain;
using System.Threading.Tasks;
using Customer_Management.Application.DTOs.Customer.Validators;
using Customer_Management.Application.Exceptions;
using Customer_Management.Application.Responses;
using System.Linq;

namespace Customer_Management.Application.Features.Customer.Handlers.Commands
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, BaseCommandResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            #region Validations
            var validator = new CreateCustomerDtoValidator(_customerRepository,_mapper);
            var validationResult = await validator.ValidateAsync(request.CustomerDto);
            if (validationResult.IsValid == false)
            {
                //throw new ValidationException(validationResult);
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }
            else
            {

                var customer = _mapper.Map<Customer_Management_Domain.Customer>(request.CustomerDto);
                customer = await _customerRepository.Add(customer);
                response.Success = true;
                response.Message = "Creation Successful";            
                response.Id = customer.Id;
            }
            #endregion



            return response;
        }
    }
}
