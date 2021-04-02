namespace Assignment.Domain
{
    public class ShoppingItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int ShoppingBagId { get; set; }
        public ShoppingBag ShoppingBag { get; set; }

        public ShoppingItem()
        {

        }

        public ShoppingItem(int quantity, int productId, int shoppingBagId)
        {
            SetQuantity(quantity);
            SetProductId(productId);
            SetShoppingBagId(shoppingBagId);
        }

        public void SetQuantity(int quantity)
        {
            Quantity = quantity;
        }

        public void SetProductId(int id)
        {
            ProductId = id;
        }

        public void SetShoppingBagId(int id)
        {
            ShoppingBagId = id;
        }
    }
}
