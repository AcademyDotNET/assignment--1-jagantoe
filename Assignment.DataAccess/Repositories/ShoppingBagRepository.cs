using Assignment.Domain;

namespace Assignment.DataAccess.Repositories
{
    public class ShoppingBagRepository : Repository<ShoppingBag>
    {
        public ShoppingBagRepository(AssignmentContext dbContext) : base(dbContext, dbContext.ShoppingBags)
        {

        }
    }
}
