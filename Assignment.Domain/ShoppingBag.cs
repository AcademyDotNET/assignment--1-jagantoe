using System;
using System.Collections.Generic;

namespace Assignment.Domain
{
    public class ShoppingBag
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Customer Customer { get; set; }
        public ICollection<ShoppingItem> ShoppingItems { get; set; }

        public ShoppingBag()
        {
            ShoppingItems = new List<ShoppingItem>();
        }

        public ShoppingBag(DateTime date): this()
        {
            SetDate(date);
        }

        public void SetDate(DateTime date)
        {
            Date = date;
        }
    }
}
