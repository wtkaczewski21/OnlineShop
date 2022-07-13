using Lab_Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab_Backend.Data.Services
{
    public interface IOrderService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);
        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);
    }
}
