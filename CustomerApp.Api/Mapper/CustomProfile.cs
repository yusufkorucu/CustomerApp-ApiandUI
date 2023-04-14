using AutoMapper;
using CustomerApp.Api.Handlers.Command;
using CustomerApp.Api.Model;
using CustomerApp.Api.Services.Dtos;

namespace CustomerApp.Api.Mapper;

public class CustomProfile : Profile
{
    public CustomProfile()
    {
        CreateMap<Customer, CustomerDetailResponseDto>().ReverseMap();
        CreateMap<Customer, CustomerAddCommand>().ReverseMap();
    }
}