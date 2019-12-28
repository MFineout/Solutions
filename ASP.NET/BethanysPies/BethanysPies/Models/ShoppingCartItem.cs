using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPies.Models
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }

        public Pie Pie { get; set; }

        public int Amount { get; set; } //# of pies added of a certain pie

        public string ShoppingCartId { get; set; }

    }
}
