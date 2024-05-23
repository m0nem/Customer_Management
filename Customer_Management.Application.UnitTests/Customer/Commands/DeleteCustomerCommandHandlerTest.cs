using AutoMapper;
using Customer_Management.Application.Features.Customer.Handlers.Commands;
using Customer_Management.Application.Features.Customer.Requests.Commands;
using Customer_Management.Application.Persistence.Contracts;
using Customer_Management.Application.Profiles;
using Customer_Management.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
namespace Customer_Management.Application.UnitTests.Customer.Commands
{

    public class DeleteCustomerCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICustomerRepository> _mockRepository;
        public DeleteCustomerCommandHandlerTest()
        {

            _mockRepository = MockCustomerRepository.GetCustomerRepository();
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = mapper.CreateMapper();
        }

        [Theory]
        [InlineData(2)]
        public async Task Delete_RemovesCustomerFromRepository(int id)
        {
            // Arrange
            var handler = new DeleteCustomerCommandHandler(_mockRepository.Object, _mapper);

            // Act
            await handler.Handle(new DeleteCustomerCommand() { Id = id }, CancellationToken.None);

            // Assert
            var result = await _mockRepository.Object.Get(id);
            result.ShouldBeNull();
        }

    }


}
