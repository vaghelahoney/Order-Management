using OrderManagement.Dto;

namespace OrderManagement.IService
{
    public interface IOrdeService
    {
        Task<IEnumerable<OrderDto>> GetAllOrderAsync(int PageNumber, int PageSize);

        Task<OrderDto> GetByOrderIdAsync(int id);

        Task<OrderDto> AddOrderAsync(OrderDto entity);

        Task<bool> UpdateOrderAsync(int id, OrderDto entity);

        Task<bool> DeleteOrderAsync(int id);
    }
}
