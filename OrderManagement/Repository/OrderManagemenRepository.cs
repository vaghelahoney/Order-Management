using Microsoft.EntityFrameworkCore;
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
            var Oedersave = await _appDbContext.Orders.AddAsync(entity);

            await _appDbContext.SaveChangesAsync();

            OrderItem orders = new OrderItem();

            if (entity.OrderItems.Count() > 0)
            {
                for (int i = 0; i < entity.OrderItems.Count(); i++)
                {
                    orders.OrderId = Oedersave.Entity.Id;
                    orders.ProductName = entity.OrderItems[i].ProductName;
                    orders.Quantity = entity.OrderItems[i].Quantity;
                    orders.Price = entity.OrderItems[i].Price;

                    await _appDbContext.OrderItems.AddAsync(orders);
                    await _appDbContext.SaveChangesAsync();
                }
            }
            return entity;
        }

        public async Task<IEnumerable<Order>> GetAllAsync(int PageNumber, int PageSize)
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
                                .Include(o => o.OrderItems).AsNoTracking()
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
                return await _appDbContext.Orders.Include(o => o.Id).FirstOrDefaultAsync(o => o.Id == id);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(Order entity)
        {
            try
            {
                var data = _appDbContext.Orders.Update(entity);
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
