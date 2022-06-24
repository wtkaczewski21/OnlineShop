using Lab_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab_Backend.Data.Services
{
    public class ProductBrandService : IProductBrandService
    {
        private readonly AppDbContext _db;
        public ProductBrandService(AppDbContext db)
        {
            _db = db;
        }
        public async Task AddAsync(ProductBrand productBrand)
        {
            await _db.ProductBrands.AddAsync(productBrand);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _db.ProductBrands.FirstOrDefaultAsync(x => x.Id == id);
            _db.ProductBrands.Remove(result);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductBrand>> GetAllAsync()
        {
            var result = await _db.ProductBrands.ToListAsync();
            return result;
        }

        public async Task<ProductBrand> GetByIdAsync(int id)
        {
            var result = await _db.ProductBrands.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<ProductBrand> UpdateAsync(int id, ProductBrand productBrand)
        {
            _db.Update(productBrand);
            await _db.SaveChangesAsync();
            return productBrand;
        }
    }
}
