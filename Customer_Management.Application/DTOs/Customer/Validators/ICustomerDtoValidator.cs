using Customer_Management.Application.Persistence.Contracts;
using FluentValidation;
using FluentValidation.Validators;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using PhoneNumbers;
namespace Customer_Management.Application.DTOs.Customer.Validators
{
    public class ICustomerDtoValidator : AbstractValidator<ICustomerDto>
    {


        public ICustomerDtoValidator()
        {
            RuleFor(p => p.FirstName)
                  .NotEmpty().WithMessage("{PropertyName} is Required.")
                  .NotNull()
                  .MaximumLength(50).WithMessage("{PropertyName}must not exceed 50");

            RuleFor(p => p.LastName)
                 .NotEmpty().WithMessage("{PropertyName} is Required.")
                 .NotNull()
                 .MaximumLength(50).WithMessage("{PropertyName}must not exceed 50");

            RuleFor(p => p.Phone)
                       .Cascade(CascadeMode.Stop)
                       .NotEmpty().WithMessage("Phone Number is required.")
                       .NotNull().WithMessage("Phone Number is required.")
                       .Must(BeValidPhoneNumber).WithMessage("Invalid phone number.");


            RuleFor(p => p.BankAccountNumber)
                .NotNull()
                .NotEmpty().WithMessage("bank Account number is required.")
                .Must(x => x.ToString().Length == 10)
                .Must(BeValidAccountNumber)
                .WithMessage("Please enter a 10-digit account number.");

        }

        private bool BeValidAccountNumber(int accountNumber)
        {
            string accountNumberStr = accountNumber.ToString();
            if (accountNumberStr.Length != 10)
                return false;

            foreach (char c in accountNumberStr)
            {
                if (!char.IsDigit(c))
                    return false;
            }

            return true;
        }

        private bool BeValidPhoneNumber(string phoneNumber)
        {

            PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
            try
            {
                PhoneNumber parsedPhoneNumber = phoneNumberUtil.Parse(phoneNumber, null);
                return phoneNumberUtil.IsValidNumber(parsedPhoneNumber) && phoneNumberUtil.GetNumberType(parsedPhoneNumber) == PhoneNumberType.MOBILE;
            }
            catch (NumberParseException)
            {
                return false;
            }
        }
    }
}
