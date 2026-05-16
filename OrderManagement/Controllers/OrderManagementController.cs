using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Dto;
using OrderManagement.IRepository;
using OrderManagement.IService;
using OrderManagement.Model;

namespace OrderManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderManagementController : ControllerBase
    {
        private readonly IOrdeService _ordeService;  
        
        public OrderManagementController(IOrdeService ordeService)
        {
            _ordeService = ordeService;
        }

        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders([FromQuery] Pagination pagination)
        {
            if (pagination.PageNumber <= 0 || pagination.PageSize <= 0)
            {
                return BadRequest();
            }
            var data  = await _ordeService.GetAllOrderAsync(pagination.PageNumber, pagination.PageSize);
            return Ok(data);
        }

        [HttpGet("GetCounties")]
        public async Task<IActionResult> GetCounties()
        {
            var counties = await _ordeService.GetallCounty();
            return Ok(counties);
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto order )
        {
            try
            {
                if (order == null || !ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (order.OrderItemsDto == null || !order.OrderItemsDto.Any())
                {
                    return BadRequest("Order must contain at least one item.");
                }

                var data = await _ordeService.AddOrderAsync(order);
                return Ok(data);

            }
            catch (Exception)
            {

                throw;
            }
            
        }


        [HttpGet("GetOrderById/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var data = await _ordeService.GetByOrderIdAsync(id);
            return Ok(data);
        }

        [HttpDelete("DeleteOrder/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }
            var data = await _ordeService.DeleteOrderAsync(id) ;
            return Ok(data);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderDto order)
        {
            if (order == null || order.Id <= 0)
            {
                return BadRequest("Invalid order data.");
            }

            var data = await _ordeService.UpdateOrderAsync(order.Id, order);
            return Ok(data);
        }
    }
}
