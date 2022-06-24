using Lab_Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab_Backend.Data.Services
{
    public interface IProductBrandService
    {
        Task<IEnumerable<ProductBrand>> GetAllAsync();
        Task<ProductBrand> GetByIdAsync(int id);
        Task AddAsync(ProductBrand productBrand);
        Task<ProductBrand> UpdateAsync(int id, ProductBrand productBrand);
        Task DeleteAsync(int id);
    }
}
