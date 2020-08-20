using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.AUTHENTICATION.API;
using  BTS.SP.AUTHENTICATION.API.BuildQuery;
using  BTS.SP.AUTHENTICATION.API.BuildQuery.Result;

namespace  BTS.SP.AUTHENTICATION.API.Services
{
    public interface IEntityService<TEntity> : IService
        where TEntity : EntityBase
    {
        IRepository<TEntity> Repository { get; }
        List<Expression<Func<TEntity, object>>> Includes { get; }

        IEntityService<TEntity> Include(Expression<Func<TEntity, object>> include);

        ResultObj<PagedObj<TEntity>> Filter<TSearch>(
        FilterObj<TSearch> filtered,
        IQueryBuilder query = null)
        where TSearch : IDataSearch;
    }
}
