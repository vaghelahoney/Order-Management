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
        public async Task<IActionResult> GetOrders([FromBody] Pagination pagination)
        {
            if (pagination.PageNumber <= 0 && pagination.PageSize <= 0)
            {
                return BadRequest();
            }
            var data  = await _orderManagemenRepository.GetAllAsync(pagination.PageNumber, pagination.PageSize);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order )
        {
            if (order == null)
            {
                return BadRequest();
            }
            var data = await _orderManagemenRepository.AddAsync(order); 
            return Ok(data);
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


        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromBody] Order order)
        {
            var data = await _orderManagemenRepository.Update(order);
            return Ok(data);
        }
    }
}
