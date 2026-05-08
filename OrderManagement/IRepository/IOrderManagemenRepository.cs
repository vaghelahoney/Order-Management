using OrderManagement.Dto;

namespace OrderManagement.IRepository
{
    public interface IOrderManagemenRepository
    {

        Task<IEnumerable<OrderDto>> GetAllAsync(int PageNumber, int PageSize);

        Task<OrderDto> GetByIdAsync(int id);

        Task<OrderDto> AddAsync(OrderDto entity);

        Task<bool> Update(int id ,OrderDto entity);

        Task<bool> DeleteOrder(int id);

    }
}
