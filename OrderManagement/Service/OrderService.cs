using AutoMapper;
using OrderManagement.Dto;
using OrderManagement.IRepository;
using OrderManagement.IService;
using OrderManagement.Mapping;
using OrderManagement.Migrations;
using OrderManagement.Model;
using OrderManagement.Repository;

namespace OrderManagement.Service
{
    public class OrderService : IOrdeService
    {
        private readonly IOrderManagemenRepository _orderManagemenRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderManagemenRepository orderManagemenRepository, IMapper mapper)
        {
            _orderManagemenRepository = orderManagemenRepository;
            _mapper = mapper;
        }

        public async Task<OrderDto> AddOrderAsync(OrderDto entity)
        {
           
            Order order = _mapper.Map<Order>(entity);

            await _orderManagemenRepository.AddAsync(order);

            return entity;

        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            return await _orderManagemenRepository.DeleteOrder(id);
        }

        public async Task<IEnumerable<CountyDto>> GetallCounty()
        {
            var counties = await _orderManagemenRepository.GetallCounty();
          return  counties.Select(c => _mapper.Map<CountyDto>(c));
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrderAsync(int PageNumber, int PageSize)
        {
            PageNumber = 1;
            PageSize = 10;

            var data = await _orderManagemenRepository.GetAllAsync(PageNumber, PageSize);

            return data.Select(x => new OrderDto
            {
                CreatedAt = x.CreatedAt,
                CustomerName = x.CustomerName,
                Id = x.Id,
                Status = x.Status,
                OrderItemsDto = x.OrderItems.Select(a => new OrderItemDto
                {
                    Id = a.Id,
                    ProductName = a.ProductName,
                    Quantity = a.Quantity,
                    Price = a.Price,
                    OrderId = a.OrderId,
                    CountryId = a.CountryId,
                    CountryName = a.Country?.CountryName
                }).ToList()
            }).ToList();
        }

        public async Task<OrderDto> GetByOrderIdAsync(int id)
        {
            var data = await _orderManagemenRepository.GetByIdAsync(id);

            return new OrderDto
            {
                CreatedAt = data.CreatedAt,
                CustomerName = data.CustomerName,
                Status = data.Status,
                Id = data.Id,
                
                OrderItemsDto = data.OrderItems.Select(s => new OrderItemDto
                {
                    Id = s.Id,
                    OrderId = s.OrderId,
                    Price = s.Price,
                    Quantity = s.Quantity,
                    ProductName = s.ProductName,
                    CountryId = s.CountryId,
                    CountryName = s.Country?.CountryName
                }).ToList()
            };
        }

        public async Task<bool> UpdateOrderAsync(int id, OrderDto entity)
        {
            
            Order order = _mapper.Map<Order>(entity);

            return await _orderManagemenRepository.Update(id, order);
        }
    }
}
