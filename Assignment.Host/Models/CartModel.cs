using System.Collections.Generic;

namespace MVC_Assignment.Models
{
    public class CartModel
    {
        public List<CartItemModel> Items { get; set; }
        public int Discount { get; set; }
    }
}
