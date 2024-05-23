using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Management.Application.UnitTests.Validators.PhoneNumber
{
    public class PhoneNumberTests
    {
        [Theory]
        [InlineData("+16156381234")]
        [InlineData("+989396080826")]
        private void IsPhoneNumberValid_WithValidPhoneNumber_ShouldReturnTrue(string phoneNumber)
        {
            //Arrange
            var handler = new PhoneNumberHelperTest();

            //Act
            var result = handler.IsPhoneNumberValid(phoneNumber);

            //Assert
            result.ShouldBeTrue();
        }


        [Theory]
        [InlineData("+1615638123")] 
        [InlineData("+986153523305")] 
        public void IsPhoneNumberValid_WithInvalidPhoneNumber_ShouldReturnFalse(string phoneNumber)
        {
            // Arrange
            var handler = new PhoneNumberHelperTest();

            // Act
            var result = handler.IsPhoneNumberValid(phoneNumber);

            // Assert
            result.ShouldBeFalse(); 
        }
    }
}
