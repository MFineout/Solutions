using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPies.Models
{
    public class ShoppingCart
    {
        private readonly AppDbContext _appDbContext;
        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        private ShoppingCart(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        
        public static ShoppingCart GetCart(IServiceProvider services) 
        {
            //gives access to services managed in the dependency injection container

            //Sessions allow me to store information on the server side between the request and to an underlying mechanism of cookies, ASP.NET can this way remember basically, keep state information on the server about a certain active session. Now to bring in support for sessions, I actually need something called the IHTTPContextAccessor.
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                               .HttpContext.Session;

            var context = services.GetService<AppDbContext>();

            //if "cartId" is null, create new cartId string
            //GetString gets the primary key value of the database table
            string cartId = session.GetString("ShoppingCartItemId") ?? Guid.NewGuid().ToString();

            session.SetString("ShoppingCartItemId", cartId); //sets KVP

            return new ShoppingCart(context) { ShoppingCartId = cartId };

        }

        public void AddToCart(Pie pie)
        {
            var shoppingCartItem =
                _appDbContext.ShoppingCartItems.SingleOrDefault
                (s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

            //if item is new in cart
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    Pie = pie,
                    Amount = 1,
                    ShoppingCartId = ShoppingCartId
                };

                _appDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else //if item is already in cart, just increase amount by 1
            {
                shoppingCartItem.Amount++;
            }

            _appDbContext.SaveChanges();   
        }

        public int RemoveFromCart(Pie pie)
        {
            var shoppingCartItem =
                _appDbContext.ShoppingCartItems.SingleOrDefault
                (s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if(shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _appDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _appDbContext.SaveChanges();
            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??
                (ShoppingCartItems =
                _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Include(s => s.Pie)
                .ToList());
        }

        public void ClearCart()
        {
            var cartItems = _appDbContext.ShoppingCartItems.
                            Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _appDbContext.ShoppingCartItems.RemoveRange(cartItems);

            _appDbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                        .Select(c => c.Pie.Price * c.Amount).Sum();
            return total;
        }
    }
}
