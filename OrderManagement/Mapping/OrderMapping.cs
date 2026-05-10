using AutoMapper;
using OrderManagement.Dto;
using OrderManagement.Model;

namespace OrderManagement.Mapping
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<OrderDto, Order>().ReverseMap();

            CreateMap<OrderItemDto, OrderItem>().ReverseMap();
        }
    }
}
