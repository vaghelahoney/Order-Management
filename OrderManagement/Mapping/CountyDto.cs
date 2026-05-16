using System.ComponentModel.DataAnnotations;

namespace OrderManagement.Mapping
{
    public class CountyDto
    {
        [Key]
        public int CountryId { get; set; }

        public string? CountryName { get; set; }
    }
}
