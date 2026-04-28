using OrderManagement.Model;

namespace OrderManagement.IRepository
{
    public interface IOrderManagemenRepository
    {

        Task<IEnumerable<Order>> GetAllAsync(int PageNumber, int PageSize);

        Task<Order> GetByIdAsync(int id);

        Task<Order> AddAsync(Order entity);

        Task<bool> Update(Order entity);

    }
}
