using AutoMapper;
using OrderManagement.Dto;
using OrderManagement.Model;

namespace OrderManagement.Mapping
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<OrderDto, Order>()
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItemsDto))
            .ReverseMap();

            CreateMap<OrderItemDto, OrderItem>().ReverseMap();
            CreateMap<CountyDto, County>().ReverseMap();

        }
    }
}
