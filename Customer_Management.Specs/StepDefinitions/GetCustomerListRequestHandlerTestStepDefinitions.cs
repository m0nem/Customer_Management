using AutoMapper;
using Customer_Management.Application.DTOs.Customer;
using Customer_Management.Application.Features.Customer.Handlers.Commands;
using Customer_Management.Application.Features.Customer.Handlers.Queries;
using Customer_Management.Application.Features.Customer.Requests.Commands;
using Customer_Management.Application.Features.Customer.Requests.Queries;
using Customer_Management.Application.Persistence.Contracts;
using Customer_Management.Application.Profiles;
using Customer_Management_Domain;
using Moq;
using Shouldly;
using System;
using TechTalk.SpecFlow;

namespace Customer_Management.Specs.StepDefinitions
{
    [Binding]
    public class GetCustomerListRequestHandlerTestStepDefinitions
    {
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;
        private List<Customer> _allCustomers;
        private List<CustomerDto> _resultCustomers; 

        private readonly IMapper _mapper;
        public GetCustomerListRequestHandlerTestStepDefinitions()
        {
            _mockCustomerRepository = new Mock<ICustomerRepository>();

            _allCustomers = new List<Customer>
            {
                new Customer { Id = 10,  FirstName = "John",    LastName = "Doe",   DateOfBirth = new DateTime(1992, 2, 2), Email = "john@example.com",      Phone = "09396080822",   BankAccountNumber =    1987654321 },
                new Customer { Id = 11,  FirstName = "REX",     LastName = "lOWA",  DateOfBirth = new DateTime(1992, 2, 2), Email = "john@example.com",      Phone = "09396080822",   BankAccountNumber =    1865325318 },
                new Customer { Id = 12,  FirstName = "ANITA",   LastName = "Ragbi", DateOfBirth = new DateTime(1992, 2, 2), Email = "john@example.com",      Phone = "09396080822",   BankAccountNumber =    1253698700 }
            };
            var mapper = new MapperConfiguration(m =>
            {
                m.AddProfile<MappingProfile>();
            });
            _mapper = mapper.CreateMapper();
        }

        [Given(@"a customer repository is available")]
        public void GivenACustomerRepositoryIsAvailable()
        {
            _mockCustomerRepository.Setup(repo => repo.GetAll()).ReturnsAsync(_allCustomers);
        }

        [When(@"I request all customers")]
        public async Task WhenIRequestAllCustomersAsync()
        {
            var handler = new GetCustomerListRequestHandler(_mockCustomerRepository.Object, _mapper);
            _resultCustomers = await handler.Handle(new GetCustomerListRequest(), CancellationToken.None);
        }

        [Then(@"all customers should be returned")]
        public void ThenAllCustomersShouldBeReturned()
        {
            _mockCustomerRepository.Verify(repo => repo.GetAll(), Times.Once);

            _resultCustomers.ShouldNotBeNull();
            _resultCustomers.Count.ShouldBe(_allCustomers.Count);

            for (int i = 0; i < _allCustomers.Count; i++)
            {
                _resultCustomers[i].Id.ShouldBe(_allCustomers[i].Id);
                _resultCustomers[i].FirstName.ShouldBe(_allCustomers[i].FirstName);
                _resultCustomers[i].LastName.ShouldBe(_allCustomers[i].LastName);
                _resultCustomers[i].DateOfBirth.ShouldBe(_allCustomers[i].DateOfBirth);
                _resultCustomers[i].Email.ShouldBe(_allCustomers[i].Email);
                _resultCustomers[i].Phone.ShouldBe(_allCustomers[i].Phone);
                _resultCustomers[i].BankAccountNumber.ShouldBe(_allCustomers[i].BankAccountNumber);
            }
        }
    }
}
