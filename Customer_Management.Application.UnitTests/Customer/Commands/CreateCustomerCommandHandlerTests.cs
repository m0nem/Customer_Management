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
    public class CreateCustomerCommandHandlerTests
    {
        IMapper _mapper;
        Mock<ICustomerRepository> _Mockrepository;
        CreateCustomerDto _customerDto;
 
        public CreateCustomerCommandHandlerTests()
        {
            _Mockrepository = MockCustomerRepository.GetCustomerRepository();

            var mapper = new MapperConfiguration(m =>
            {
                m.AddProfile<MappingProfile>();
            });
            _mapper = mapper.CreateMapper();
            _customerDto = new CreateCustomerDto()
            {
                BankAccountNumber = 1234567890,
                DateOfBirth = DateTime.Now,
                Email = "monem@gmial.com",
                FirstName = "monem",
                LastName = "mousavi",
                Phone = "+989396080826"
            };

        }

        [Fact]
        public async Task CreateCustomer()
        {
            var handler = new CreateCustomerCommandHandler(_Mockrepository.Object, _mapper);
            var result = await handler.Handle(new CreateCustomerCommand()
            {
                CustomerDto = _customerDto
            }, CancellationToken.None);

            result.Errors.Count().ShouldBe(0);
            result.ShouldBeOfType<BaseCommandResponse>();
            var customers = await _Mockrepository.Object.GetAll();
            customers.Count.ShouldBe(4);

        }

    }
}
