﻿namespace Customer_Management.MVC.Services.Base
{
    public class Response<T>
    {
        public string Message { get; set; }
        public string ValidationErrors  { get; set; }
        public bool Success { get; set; }
        public T Date { get; set; }
    }
}
