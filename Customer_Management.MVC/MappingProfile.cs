using AutoMapper;
using Customer_Management.MVC.Models.Customer;
using Customer_Management.MVC.Services.Base;

namespace Customer_Management.MVC
{
    public class MappingProfile:Profile
    {

        public MappingProfile()
        {
            CreateMap<CreateCustomerDto, CreateCustomerVM>().ReverseMap();
            CreateMap<CustomerDto,CustomerVM>().ReverseMap();
        }
    }
}
