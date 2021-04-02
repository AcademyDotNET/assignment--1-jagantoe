namespace Assignment.Domain
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public int ShoppingBagId { get; set; }
        public ShoppingBag ShoppingBag { get; set; }

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
