using Lab_Backend.Models;
using System.Collections.Generic;

namespace Lab_Backend.Data.ViewModels
{
    public class ProductDropdownsVM
    {
        public ProductDropdownsVM()
        {
            ProductBrands = new List<ProductBrand>();
        }
        public List<ProductBrand> ProductBrands { get; set; }
    }
}
