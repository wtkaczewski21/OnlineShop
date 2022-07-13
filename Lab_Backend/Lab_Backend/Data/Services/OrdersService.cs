using Lab_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_Backend.Data.Services
{
    public class OrdersService : IOrderService
    {
        private readonly AppDbContext _db;
        public OrdersService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {
            var orders = await _db.Orders.Include(x => x.OrderItems).ThenInclude(x => x.Product).Include(x => x.User).ToListAsync();

            if (userRole != "Admin")
            {
                orders = orders.Where(x => x.UserId == userId).ToList();
            }

            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress
            };
            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    ProductId = item.Product.Id,
                    OrderId = order.Id,
                    Price = item.Product.Price
                };
                await _db.OrderItems.AddAsync(orderItem);
            }
            await _db.SaveChangesAsync();
        }
    }
}
