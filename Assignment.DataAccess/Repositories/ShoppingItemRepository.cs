using Assignment.Domain;

namespace Assignment.DataAccess.Repositories
{
    public class ShoppingItemRepository: Repository<ShoppingItem>
    {
        public ShoppingItemRepository(AssignmentContext dbContext) : base(dbContext, dbContext.ShoppingItems)
        {

        }
    }
}
