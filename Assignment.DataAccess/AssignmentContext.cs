using Assignment.Domain;
using Microsoft.EntityFrameworkCore;

namespace Assignment.DataAccess
{
    public class AssignmentContext: DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingBag> ShoppingBags { get; set; }
        public DbSet<ShoppingItem> ShoppingItems { get; set; }

        public AssignmentContext(DbContextOptions options) : base(options)
        {

        }
    }
}
