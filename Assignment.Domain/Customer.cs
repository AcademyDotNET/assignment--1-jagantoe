namespace Assignment.Domain
{
    public class Customer
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string PasswordSalt { get; private set; }
        public int ShoppingBagId { get; private set; }
        public ShoppingBag ShoppingBag { get; private set; }

        public Customer()
        {

        }

        public Customer(string firstname, string lastname, string email, string passwordHash, string passwordSalt, int shoppingbagId)
        {
            SetFirstName(firstname);
            SetLastName(lastname);
            SetEmail(email);
            SetPasswordHash(passwordHash);
            SetPasswordSalt(passwordSalt);
            SetShoppingBagId(shoppingbagId);
        }

        public void SetFirstName(string firstname)
        {
            FirstName = firstname;
        }

        public void SetLastName(string lastname)
        {
            LastName = lastname;
        }

        public void SetEmail(string email)
        {
            Email = email;
        }

        public void SetPasswordHash(string password)
        {
            PasswordHash = password;
        }

        public void SetPasswordSalt(string salt)
        {
            PasswordSalt = salt;
        }

        public void SetShoppingBagId(int id)
        {
            ShoppingBagId = id;
        }
    }
}
