
using Customer_Management.Application.Persistence.Contracts;
using Customer_Management_Domain;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Customer_Management.Application.DTOs.Customer.Validators
{
    public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerValidator(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            Include(new ICustomerDtoValidator());


            RuleFor(s => s.Email).NotEmpty().WithMessage("Email address is required")
                     .EmailAddress().WithMessage("A valid email is required");

            RuleFor(customer => customer).MustAsync(async (updateCustomerDto, token) =>
            {
                      var emailExist = await EmailExists(updateCustomerDto);
                      return !emailExist;
             }).WithMessage("This email address is already in use");
        }

        private async Task<bool> EmailExists(UpdateCustomerDto updateCustomerDto)
        {
            var emailExist = await _customerRepository.ExistEmail(updateCustomerDto.Email);
            if (emailExist)
            {
                var getCustomer = await _customerRepository.GetCustomerByEmail(updateCustomerDto.Email);
                if (getCustomer.Id == updateCustomerDto.Id)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
