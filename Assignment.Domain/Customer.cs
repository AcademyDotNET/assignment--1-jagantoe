namespace Assignment.Domain
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ShoppingBagId { get; set; }
        public ShoppingBag ShoppingBag { get; set; }
    }
}
