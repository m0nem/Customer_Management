using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Management.Application.UnitTests.Customer.Commands.Validator
{
    public class PhoneNumberTests
    {

        public static bool BeValidPhoneNumber(string phone)
        {
            PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
            try
            {
                PhoneNumber parsedPhoneNumber = phoneNumberUtil.Parse(phone, null);
                var isValid = phoneNumberUtil.IsValidNumber(parsedPhoneNumber) && phoneNumberUtil.GetNumberType(parsedPhoneNumber) == PhoneNumberType.FIXED_LINE_OR_MOBILE;
                return isValid;
            }
            catch (NumberParseException)
            {
                return false;
            }
        }

    }
}
