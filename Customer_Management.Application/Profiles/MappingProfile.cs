using AutoMapper;
using Customer_Management.Application.DTOs.Customer;
using Customer_Management_Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer_Management.Application.Profiles
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            #region Customer Mapping
            CreateMap<Customer,CustomerDto>().ReverseMap();
            CreateMap<Customer,CreateCustomerDto>().ReverseMap();
            CreateMap<Customer,UpdateCustomerDto>().ReverseMap();

            #endregion
        }
    }
}
