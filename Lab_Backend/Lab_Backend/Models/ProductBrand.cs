using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab_Backend.Models
{
    public class ProductBrand
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Logo { get; set; }

        //Relationships:
        public List<Product> Products { get; set; }
    }
}
