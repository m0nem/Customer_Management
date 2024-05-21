using AutoMapper;
using Customer_Management.Application.DTOs.Customer;
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
    public class GetCustomerListRequestHandlerTests
    {
        readonly IMapper _mapper;
        readonly Mock<ICustomerRepository> _mockRepository;
        public GetCustomerListRequestHandlerTests()
        {
            _mockRepository = MockCustomerRepository.GetCustomerRepository();
            var mapperConfig = new MapperConfiguration(m =>
            {
                m.AddProfile<MappingProfile>();

            });

            _mapper = mapperConfig.CreateMapper();
        }


        [Fact]
        public async Task GetCustomerListTest()
        {
            var handler = new GetCustomerListRequestHandler(_mockRepository.Object, _mapper);
            var result = await handler.Handle(new GetCustomerListRequest(), CancellationToken.None);
            result.ShouldBeOfType<List<CustomerDto>>();
            result.Count.ShouldBe(3);
        }
    }
}
