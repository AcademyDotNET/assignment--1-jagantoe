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
    }
}
