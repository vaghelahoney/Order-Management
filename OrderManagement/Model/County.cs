using System.ComponentModel.DataAnnotations;

namespace OrderManagement.Model
{
    public class County
    {
        [Key]
        public int CountryId { get; set; }

        public string? CountryName { get; set; }
    }
}
