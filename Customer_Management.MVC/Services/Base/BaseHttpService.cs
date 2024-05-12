using Customer_Management.MVC.Contracts;
using Hanssens.Net;
using System.Net.Http.Headers;

namespace Customer_Management.MVC.Services.Base
{
    public class BaseHttpService
    {
        protected readonly ILocalStorageService _localStorageService;
        protected readonly IClient _client;

        public BaseHttpService(IClient client, ILocalStorageService localStorageService)
        {
            _client = client;
            _localStorageService = localStorageService;
        }

        protected Response<Guid> ConvertApiExceptions<Guid>(ApiException exception)
        {
            if (exception.StatusCode == 400)
            {
                return new Response<Guid> { Message = "Validation errors have occured", ValidationErrors = exception.Response, Success = false };
            }
            else if (exception.StatusCode == 404)
            {
                return new Response<Guid> { Message = "Not Found ...", Success = false };
            }
            else
            {
                return new Response<Guid> { Message = "Somthing went wrong,try again ...", Success = false };
            }
        }

        protected void AddBearerToken() 
        {
            if (_localStorageService.Exists("token")) 
            {
                _client.HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer",_localStorageService.GetStorageValue<string>("token"));
            }
        }
    }
}
