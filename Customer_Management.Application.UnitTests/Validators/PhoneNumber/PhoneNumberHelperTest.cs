using PhoneNumbers;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Management.Application.UnitTests.Validators.PhoneNumber
{
    public class PhoneNumberHelperTest
    {

        private readonly PhoneNumberUtil _phoneNumberUtil;

        public PhoneNumberHelperTest()
        {
            _phoneNumberUtil = PhoneNumberUtil.GetInstance();
        }

        public bool IsPhoneNumberValid(string phone)
        {
            try
            {
                var parsedPhoneNumber = _phoneNumberUtil.Parse(phone, null);
                return _phoneNumberUtil.IsValidNumber(parsedPhoneNumber) &&
                       (_phoneNumberUtil.GetNumberType(parsedPhoneNumber) == PhoneNumberType.MOBILE ||
                        _phoneNumberUtil.GetNumberType(parsedPhoneNumber) == PhoneNumberType.FIXED_LINE);
            }
            catch (NumberParseException)
            {
                return false;
            }
        }

    }
}
