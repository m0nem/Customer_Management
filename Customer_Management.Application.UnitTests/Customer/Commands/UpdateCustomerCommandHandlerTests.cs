using AutoMapper;
using Customer_Management.Application.DTOs.Customer;
using Customer_Management.Application.Features.Customer.Handlers.Commands;
using Customer_Management.Application.Features.Customer.Requests.Commands;
using Customer_Management.Application.Persistence.Contracts;
using Customer_Management.Application.Profiles;
using Customer_Management.Application.Responses;
using Customer_Management.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Management.Application.UnitTests.Customer.Commands
{
    public class UpdateCustomerCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICustomerRepository> _mockRepository;
        public UpdateCustomerCommandHandlerTests()
        {
            _mockRepository = MockCustomerRepository.GetCustomerRepository();
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = mapper.CreateMapper();
        }

        [Theory]
        [InlineData(1,"reza", "atari", "+989396080800", "monsdfem@gmail.com", 1234567890)]
        public async Task Update_UpdatesCustomerInRepository(int id,string firstName, string lastName, string phone, string email, int bankAccountNumber)
        {
            // Arrange
            var customerDto = new UpdateCustomerDto
            {
                Id=id,
                FirstName = firstName,
                LastName = lastName,
                Phone = phone,
                Email = email,
                DateOfBirth = DateTime.Now,
                BankAccountNumber = bankAccountNumber
            };

            // Act
            var handler = new UpdateCustomerCommandHandler(_mockRepository.Object, _mapper);

            var result = await handler.Handle(new UpdateCustomerCommand
            {
              Customer=customerDto
            }, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<BaseCommandResponse>();
            var modify = await _mockRepository.Object.Get(id);
            modify.FirstName.ShouldBe(customerDto.FirstName);
            modify.LastName.ShouldBe(customerDto.LastName);
        }

    }
}
