using AutoMapper;
using Customer_Management.Application.Features.Customer.Handlers.Queries;
using Customer_Management.Application.Features.Customer.Requests.Queries;
using Customer_Management.Application.Persistence.Contracts;
using Customer_Management.Application.Profiles;
using Customer_Management.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Management.Application.UnitTests.Customer.Queries
{
    public class GetCustomerDetailRequestHandlerTests
    {

        readonly IMapper _mapper;
        readonly Mock<ICustomerRepository> _mockRepository;
        public GetCustomerDetailRequestHandlerTests()
        {
            _mockRepository = MockCustomerRepository.GetCustomerRepository();

            var mapperConfig = new MapperConfiguration(m =>
            {
                m.AddProfile<MappingProfile>();

            });

            _mapper = mapperConfig.CreateMapper();
        }


        [Theory]
        [InlineData(3)]
        public async Task GetById_ReturnsCorrectCustomer(int customerId)
        {

            // Arrange
            var handler = new GetCustomerDetailRequestHandler(_mockRepository.Object, _mapper);

            // Act
            var result = await handler.Handle(new GetCustomerDetailRequest() { Id = customerId }, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(customerId);
        }

    }
}
