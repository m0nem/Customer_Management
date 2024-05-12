
using Customer_Management.Application.Persistence.Contracts;
using FluentValidation;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Threading;
using AutoMapper;

namespace Customer_Management.Application.DTOs.Customer.Validators
{
    public class CreateCustomerDtoValidator : AbstractValidator<CreateCustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CreateCustomerDtoValidator(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            Include(new ICustomerDtoValidator());

            RuleFor(s => s.Email).NotEmpty().WithMessage("Email address is required")
                     .EmailAddress().WithMessage("A valid email is required")
            .MustAsync(async (email, token) =>
            {
                var emailExist = await _customerRepository.ExistEmail(email);
                return !emailExist;
            }).WithMessage("This email address is already in use");


            RuleFor(customer => customer)
               .MustAsync(BeUniqueCustomer)
                .WithMessage("Customer with the same First Name, Last Name, and Date of Birth already exists.");
        }


        private async Task<bool> BeUniqueCustomer(CreateCustomerDto customer, CancellationToken cancellationToken)
        {
            var customerMappin = _mapper.Map<Customer_Management_Domain.Customer>(customer);
            var result = await _customerRepository.CustomerExists(customerMappin);
            return !result;
        }

    }
}
