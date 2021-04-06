using System;
using System.Collections.Generic;

namespace Assignment.Domain
{
    public class ShoppingBag
    {
        public int Id { get; private set; }
        public DateTime Date { get; private set; }
        public Customer Customer { get; private set; }
        public ICollection<ShoppingItem> ShoppingItems { get; private set; }

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
