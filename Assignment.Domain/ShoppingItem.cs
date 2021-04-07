namespace Assignment.Domain
{
    public class ShoppingItem
    {
        public int Id { get; private set; }
        public int Quantity { get; private set; }
        public int ProductId { get; private set; }
        public Product Product { get; private set; }
        public int ShoppingBagId { get; private set; }
        public ShoppingBag ShoppingBag { get; private set; }

        public ShoppingItem()
        {

        }

        public ShoppingItem(int productId, int quantity, int shoppingBagId)
        {
            SetProductId(productId);
            SetQuantity(quantity);
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
