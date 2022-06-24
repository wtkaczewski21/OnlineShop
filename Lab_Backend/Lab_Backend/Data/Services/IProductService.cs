using Lab_Backend.Data.ViewModels;
using Lab_Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab_Backend.Data.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<ProductDropdownsVM> GetDropdownsValues();
        Task AddAsync(Product product);
        Task<Product> UpdateAsync(int id, Product product);
        Task DeleteAsync(int id);
    }
}
