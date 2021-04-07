namespace Assignment.Domain
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Price { get; private set; }
        public string Registration { get; private set; }
        public Product(string name, int price, string registration)
        {
            SetName(name);
            SetPrice(price);
            SetRegistration(registration);
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetPrice(int price)
        {
            Price = price;
        }

        public void SetRegistration(string registration)
        {
            Registration = registration;
        }
    }
}
