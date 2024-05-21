using AutoMapper;
using Customer_Management.MVC.Contracts;
using Customer_Management.MVC.Models.Customer;
using Customer_Management.MVC.Services.Base;

namespace Customer_Management.MVC.Services
{
    public class CustomerService : BaseHttpService, ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IClient _httpClient;


        public CustomerService(IMapper mapper, IClient httpClient, ILocalStorageService localStorage) : base(httpClient, localStorage)
        {
            _mapper = mapper;
            _httpClient = httpClient;
        }
        public async Task<Response<int>> CreateCustomer(CreateCustomerVM customer)
        {
            try
            {
                var response = new Response<int>();
                CreateCustomerDto createCustomerDto = _mapper.Map<CreateCustomerDto>(customer);

                //TODO Auth
                AddBearerToken();
                var apiResponse = await _httpClient.CustomerPOSTAsync(createCustomerDto);
                if (apiResponse.Success)
                {
                    response.Date = apiResponse.Id;
                    response.Success = true;
                }
                else
                {
                    foreach (var err in apiResponse.Errors)
                    {
                        response.ValidationErrors += err + Environment.NewLine;
                    }
                }
                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }
        public async Task<Response<int>> DeleteCustomer(int id)
        {
            try
            {
                AddBearerToken();
                await _httpClient.CustomerDELETEAsync(id);
                return new Response<int> { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }

        }
        public async Task<List<CustomerVM>> GetCustomers()
        {
            AddBearerToken();
            var customers = await _httpClient.CustomerAllAsync();
            return _mapper.Map<List<CustomerVM>>(customers);
        }
        public async Task<CustomerVM> GetCustomerDetails(int id)
        {
            AddBearerToken();
            var customer = await _httpClient.CustomerGETAsync(id);
            return _mapper.Map<CustomerVM>(customer);
        }
        public async Task<Response<int>> UpdateCustomer(int id, CustomerVM customer)
        {
            try
            {
                var response = new Response<int>(); 
                var customerDto = _mapper.Map<CustomerDto>(customer);
                AddBearerToken();
                var apiResponse = await _httpClient.CustomerPUTAsync(id, customerDto);

                if (apiResponse.Success)
                {
                    response.Date = apiResponse.Id;
                    response.Success = true;
                }
                else
                {
                    foreach (var err in apiResponse.Errors)
                    {
                        response.ValidationErrors += err + Environment.NewLine;
                    }
                }
                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }

    }
}
