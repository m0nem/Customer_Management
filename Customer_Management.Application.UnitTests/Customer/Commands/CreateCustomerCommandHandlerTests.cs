﻿using AutoMapper;
using Customer_Management.Application.DTOs.Customer;
using Customer_Management.Application.DTOs.Customer.Validators;
using Customer_Management.Application.Features.Customer.Handlers.Commands;
using Customer_Management.Application.Features.Customer.Requests.Commands;
using Customer_Management.Application.Persistence.Contracts;
using Customer_Management.Application.Profiles;
using Customer_Management.Application.Responses;
using Customer_Management.Application.UnitTests.Mocks;
using Moq;
using PhoneNumbers;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Management.Application.UnitTests.Customer.Commands
{
    public class CreateCustomerCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICustomerRepository> _mockRepository;

        public CreateCustomerCommandHandlerTests()
        {
            _mockRepository = MockCustomerRepository.GetCustomerRepository();
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = mapper.CreateMapper();
        }

        [Theory]
        [InlineData("monesdfm", "mosdfusavi", "+989396080826", "monsdfem@gmail.com", 1234567890)]
        public async Task CreateCustomer_ShouldValidateInput(string firstName, string lastName, string phone, string email, int bankAccountNumber)
        {
            // Arrange
            var customerDto = new CreateCustomerDto
            {
                FirstName = firstName,
                LastName = lastName,
                Phone = phone,
                Email = email,
                DateOfBirth = DateTime.Now,
                BankAccountNumber = bankAccountNumber
            };

            var handler = new CreateCustomerCommandHandler(_mockRepository.Object, _mapper);

            // Act
            var result = await handler.Handle(new CreateCustomerCommand
            {
                CustomerDto = customerDto
            }, CancellationToken.None);


            // Assert
            result.ShouldBeOfType<BaseCommandResponse>();
            var customers = await _mockRepository.Object.Get(result.Id);

            // Additional assertions to validate the customer properties
            customers.FirstName.ShouldBe(firstName);
            customers.LastName.ShouldBe(lastName);
            customers.Phone.ShouldBe(phone);
            customers.Email.ShouldBe(email);
            customers.BankAccountNumber.ShouldBe(bankAccountNumber);

        }


    }
}
