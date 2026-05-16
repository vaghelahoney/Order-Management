using Microsoft.AspNetCore.Http.HttpResults;
using OrderManagement.Mapping;
using OrderManagement.Migrations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace OrderManagement.Model
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "CustomerName is required.")]

        public string? CustomerName { get; set; }

        [Required(ErrorMessage = "CreatedAt is required.")]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public bool Status { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}


