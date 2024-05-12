using AutoMapper;
using Customer_Management.Application.DTOs.Customer;
using Customer_Management.Application.Features.Customer.Handlers.Commands;
using Customer_Management.Application.Features.Customer.Requests.Commands;
using Customer_Management.Application.Persistence.Contracts;
using Customer_Management.Application.Profiles;
using Customer_Management_Domain;
using Moq;
using Shouldly;
using TechTalk.SpecFlow.Assist;

namespace Customer_Management.Specs.StepDefinitions
{
    [Binding]
    public class CreatingACustomerStepDefinitions
    {
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;
        private readonly IMapper _mapper;
        private CreateCustomerDto _command;
        private int _customerId;
        public CreatingACustomerStepDefinitions()
        {
            _mockCustomerRepository = new Mock<ICustomerRepository>();
            _mockCustomerRepository.Setup(r => r.Add(It.IsAny<Customer>())).ReturnsAsync((Customer customer) =>
                {
                    return customer;
                });

            var mapper = new MapperConfiguration(m =>
            {
                m.AddProfile<MappingProfile>();
            });
            _mapper = mapper.CreateMapper();
        }

        [Given(@"a customer repository")]
        public void GivenACustomerRepository()
        {
            // No additional action needed, mock repository is already initialized
        }

        [Given(@"a new customer with details:")]
        public void GivenANewCustomerWithDetails(Table table)
        {
             _command = table.CreateInstance<CreateCustomerDto>();
        }

        [When(@"the create customer command is handled")]
        public async Task WhenTheCreateCustomerCommandIsHandledAsync()
        {
            var handler = new CreateCustomerCommandHandler(_mockCustomerRepository.Object, _mapper);
            var result = await handler.Handle(new CreateCustomerCommand()
            {
                CustomerDto = _command
            }, CancellationToken.None);
            _customerId = result.Id;
        }

        [Then(@"the customer repository should add the new customer")]
        public void ThenTheCustomerRepositoryShouldAddTheNewCustomer()
        {
            _mockCustomerRepository.Verify(repo => repo.Add(It.IsAny<Customer>()), Times.Once);
        }

        [Then(@"the result should indicate success")]
        public void ThenTheResultShouldIndicateSuccess()
        {
            _customerId.ShouldBeOfType<int>();
            _customerId.ShouldBe(10);
        }
    }
}
