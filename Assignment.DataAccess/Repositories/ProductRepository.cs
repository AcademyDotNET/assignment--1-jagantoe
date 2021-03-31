using Assignment.Domain;

namespace Assignment.DataAccess.Repositories
{
    public class ProductRepository : Repository<Product>
    {
        public ProductRepository(AssignmentContext dbContext) : base(dbContext, dbContext.Products)
        {

        }
    }
}
