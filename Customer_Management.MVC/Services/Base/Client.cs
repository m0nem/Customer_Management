﻿
using System.Net.Http;

namespace Customer_Management.MVC.Services.Base
{
    public partial class Client : IClient
    {
        public HttpClient HttpClient 
        {
            get  
            {
                return _httpClient;
            }
        }
    }
}
