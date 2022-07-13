using Lab_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_Backend.Data.Cart
{
    public class ShoppingCart
    {
        public AppDbContext _db { get; set; }
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(AppDbContext db)
        {
            _db = db;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddItemToCart(Product product)
        {
            var shoppingCartItem = _db.ShoppingCartItems.FirstOrDefault(x => x.Product.Id == product.Id && x.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Amount = 1
                };

                _db.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
                shoppingCartItem.Amount++;

            _db.SaveChanges();
        }

        public void RemoveItemFromCart(Product product)
        {
            var shoppingCartItem = _db.ShoppingCartItems.FirstOrDefault(x => x.Product.Id == product.Id && x.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                    shoppingCartItem.Amount--;

                else
                    _db.ShoppingCartItems.Remove(shoppingCartItem);
            }
            _db.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _db.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppingCartId).Include(x => x.Product).ToList());
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _db.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppingCartId).Select(x => x.Product.Price * x.Amount).Sum();
            return total;
        }

        public async Task ClearShoppingCartAsync()
        {
            var items = await _db.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppingCartId).ToListAsync();
            _db.ShoppingCartItems.RemoveRange(items);
            await _db.SaveChangesAsync();
        }
    }
}
