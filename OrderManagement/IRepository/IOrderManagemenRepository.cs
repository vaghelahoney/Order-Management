using OrderManagement.Dto;
using OrderManagement.Model;

namespace OrderManagement.IRepository
{
    public interface IOrderManagemenRepository
    {
        Task<IEnumerable<Order>> GetAllAsync(int PageNumber, int PageSize);

        Task<Order> GetByIdAsync(int id);

        Task<Order> AddAsync(Order entity);

        Task<bool> Update(int id ,Order entity);

        Task<bool> DeleteOrder(int id);

        Task<IEnumerable<County>> GetallCounty();
    }
}
