using AutoMapper;

using web.Entities;
using web.DTO;

namespace web.Helpers;

public class AutoMapper : Profile
{
    public AutoMapper()
    {
        //for the linq to Entities(Read)
        CreateMap<Customer,UserDTO>()
        .ForMember(d => d.Email, opt => opt.MapFrom(src => src.Email))
        .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(dest=>dest.Orders , opt=> opt.MapFrom(src=> src.Orders));

        CreateMap<Order, UsersOrderDTO>()
       .ForMember(d => d.OrderName, opt => opt.MapFrom(src => src.OrderName))
       .ForMember(d => d.OrderlineItem, opt => opt.MapFrom(src => src.OrderlineItem));

        // for the Linq to objects(Add,Delete,Update)
        CreateMap<RegisterRequestDTO, Customer>()
       .ForMember(d => d.Email, opt => opt.MapFrom(src => src.Email))
       .ForMember(d => d.Name, opt => opt.MapFrom(src => src.UserName));

        CreateMap<UpdateRequest, Customer>()
       .ForMember(d => d.Email, opt => opt.MapFrom(src => src.Email))
       .ForMember(d => d.Name, opt => opt.MapFrom(src => src.UserName));

        CreateMap<OrderCreateRequestDTO,Order>();

       

    }
}

