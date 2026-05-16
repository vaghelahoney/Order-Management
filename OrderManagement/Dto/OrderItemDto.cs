using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagement.Dto
{
    public class OrderItemDto
    {
        public int Id { get; set; }

        public string? ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public int OrderId { get; set; }

        public int CountryId { get; set; }

        public string? CountryName { get; set; }
    }
}
