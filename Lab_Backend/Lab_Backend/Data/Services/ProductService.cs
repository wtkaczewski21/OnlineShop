using Lab_Backend.Data.ViewModels;
using Lab_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_Backend.Data.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _db;
        public ProductService(AppDbContext db)
        {
            _db = db;
        }
        public async Task AddAsync(Product product)
        {
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
            _db.Products.Remove(result);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var result = await _db.Products.ToListAsync();
            return result;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var result = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<ProductDropdownsVM> GetDropdownsValues()
        {
            var response = new ProductDropdownsVM()
            {
                ProductBrands = await _db.ProductBrands.OrderBy(x => x.Name).ToListAsync()
            };
            return response;
        }

        public async Task<Product> UpdateAsync(int id, Product product)
        {
            _db.Update(product);
            await _db.SaveChangesAsync();
            return product;
        }
    }
}
