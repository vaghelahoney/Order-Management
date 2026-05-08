using OrderManagement.Model;
using System.ComponentModel.DataAnnotations;

namespace OrderManagement.Dto
{
    public record OrderDto
    {

        public int Id { get; set; }

        public string? CustomerName { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool Status { get; set; }

        public List<OrderItemDto> OrderItemsDto { get; set; } = new List<OrderItemDto>();
    }
}
