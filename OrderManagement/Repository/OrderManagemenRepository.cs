using Microsoft.EntityFrameworkCore;
using OrderManagement.Dto;
using OrderManagement.IRepository;
using OrderManagement.Model;
using System.Reflection.Metadata;

namespace OrderManagement.Repository
{
    public class OrderManagemenRepository : IOrderManagemenRepository
    {

        private readonly AppDbContext _appDbContext;

        public OrderManagemenRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public async Task<Order> AddAsync(Order entity)
        {
            using var transaction = await _appDbContext.Database.BeginTransactionAsync();
            try
            {

                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                await _appDbContext.Orders.AddAsync(entity);

                await _appDbContext.SaveChangesAsync();
                
                await transaction.CommitAsync();

                return entity;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }

        }

        public async Task<bool> DeleteOrder(int id)
        {
            try
            {
                var order = await _appDbContext.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id);

                if (order == null)
                {
                    return false;
                }

                _appDbContext.OrderItems.RemoveRange(order.OrderItems);
                _appDbContext.Orders.Remove(order);

                var result = await _appDbContext.SaveChangesAsync();

                return result > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Order>> GetAllAsync(int PageNumber, int PageSize)
        {

            return await GetAllWithdtAsync(PageNumber, PageSize);
        }

        public async Task<IEnumerable<County>> GetallCounty()
        {
            try
            {

            return await _appDbContext.Countries.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Order>> GetAllWithdtAsync(int PageNumber , int PageSize)
        {
         
            if (PageNumber <= 0)
            {
                throw new ArgumentException("PageNumber must be greater than 0.");
            }

            try
            {
                return await _appDbContext.Orders
                                .Skip((PageNumber - 1) * PageSize)
                                .Take(PageSize)
                                .Include(o => o.OrderItems).ThenInclude(c => c.Country).AsNoTracking()
                                .ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }



        public async Task<Order> GetByIdAsync(int id)
        {
            try
            {
                return await _appDbContext.Orders.Include(o => o.OrderItems).ThenInclude(c=>c.Country).FirstOrDefaultAsync(o => o.Id == id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(int id, Order entity)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("Invalid ID");
                if (entity == null) throw new ArgumentNullException(nameof(entity));

                var existingOrder = await _appDbContext.Orders
                                                     .Include(o => o.OrderItems)
                                                     .FirstOrDefaultAsync(o => o.Id == id);

                if (existingOrder == null) throw new ArgumentException("Order not found");

                existingOrder.CustomerName = entity.CustomerName;
                existingOrder.CreatedAt = entity.CreatedAt;
                existingOrder.Status = entity.Status;

                foreach (var incomingItem in entity.OrderItems)
                {
                    var existingItem = existingOrder.OrderItems.FirstOrDefault(x => x.Id == incomingItem.Id);

                    if (existingItem != null)
                    {
                        existingItem.CountryId = existingItem.CountryId;
                        existingItem.Price = incomingItem.Price;
                        existingItem.ProductName = incomingItem.ProductName;
                        existingItem.Quantity = incomingItem.Quantity;
                    }
                }

                await _appDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
