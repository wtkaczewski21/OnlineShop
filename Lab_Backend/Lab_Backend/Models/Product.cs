using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab_Backend.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Image { get; set; }

        //Relationships
        [DisplayName("Brand")]
        public int ProductBrandId { get; set; }
        [ForeignKey("ProductBrandId")]
        public ProductBrand ProductBrand { get; set; }
    }
}
