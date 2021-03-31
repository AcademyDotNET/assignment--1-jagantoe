using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Assignment.DataAccess.Repositories.Interfaces
{
    public interface IRepository<TModel>
    {
        Task<IList<TModel>> All();
        Task<IList<TModel>> All(Expression<Func<TModel, bool>> predicate);

        Task<TModel> Single(Expression<Func<TModel, bool>> predicate);
        Task<TModel> Single(Expression<Func<TModel, bool>> predicate, Func<IQueryable<TModel>, IQueryable<TModel>> includes);

        Task<TModel> Create(TModel toCreate);
        Task<IList<TModel>> Create(IList<TModel> toCreate);

        Task<TModel> Update(TModel toUpdate);
        Task<IList<TModel>> Update(IList<TModel> toUpdate);

        Task Delete(TModel toDelete);
        Task Delete(IList<TModel> toDelete);

        Task<bool> Any();
        Task<bool> Any(Expression<Func<TModel, bool>> predicate);
    }
}
