using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagement.IRepository;
using OrderManagement.Model;

namespace OrderManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderManagementController : ControllerBase
    {
        private readonly IOrderManagemenRepository _orderManagemenRepository;  
        
        public OrderManagementController(IOrderManagemenRepository orderManagemenRepository)
        {
            _orderManagemenRepository = orderManagemenRepository;
        }

        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders([FromQuery] Pagination pagination)
        {
            if (pagination.PageNumber <= 0 && pagination.PageSize <= 0)
            {
                return BadRequest();
            }
            var data  = await _orderManagemenRepository.GetAllAsync(pagination.PageNumber, pagination.PageSize);
            return Ok(data);
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] Order order )
        {
            try
            {
                if (order == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (order.OrderItems == null || !order.OrderItems.Any())
                {
                    return BadRequest("Order must contain at least one item.");
                }

                var data = await _orderManagemenRepository.AddAsync(order);
                return Ok(data);

            }
            catch (Exception)
            {

                throw;
            }
            
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
           

            if (id <= 0)
            {
                return BadRequest();
            }
            var data = await _orderManagemenRepository.GetByIdAsync(id);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }
            var data = await _orderManagemenRepository.DeleteOrder(id) ;
            return Ok(data);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromBody] Order order)
        {
            if (order == null || order.Id <= 0)
            {
                return BadRequest("Invalid order data.");
            }

            var data = await _orderManagemenRepository.Update(order.Id, order);
            return Ok(data);
        }
    }
}
