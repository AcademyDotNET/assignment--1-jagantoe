using Assignment.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment.Logic.Helpers
{
    public interface IDiscountCalculator
    {
        int Calculate(IEnumerable<ShoppingItem> items);
    }
    public class AmountBasedDiscountCalculator : IDiscountCalculator
    {
        public int Calculate(IEnumerable<ShoppingItem> items)
        {
            // 5% discount for every 3 items bought, max at 50%
            double discountPercentage = 0.05; // 5% discount
            double discountCount = 3; // Every 3 items
            double maxDiscount = 0.5; // Max 50%

            var amount = items.Sum(_ => _.Quantity);

            var total = items.Sum(_ => _.Quantity * _.Product.Price);

            var discount = Math.Floor(amount / discountCount) * discountPercentage;

            discount = Math.Min(discount, maxDiscount);

            return Convert.ToInt32(total * discount);
        }
    }
}
