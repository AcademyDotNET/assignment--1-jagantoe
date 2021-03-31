using Assignment.Domain;

namespace Assignment.DataAccess.Repositories
{
    public class CustomerRepository : Repository<Customer>
    {
        public CustomerRepository(AssignmentContext dbContext) : base(dbContext, dbContext.Customers)
        {

        }
    }
}
