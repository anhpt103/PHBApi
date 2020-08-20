using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.AUTHENTICATION.API;

namespace  BTS.SP.AUTHENTICATION.API.Services
{
    public interface IDataInfoService<TEntity> : IEntityService<TEntity>
        where TEntity : DataInfoEntity
    {
        new IDataInfoService<TEntity> Include(Expression<Func<TEntity, object>> include);
        TEntity Find(TEntity instance, bool notracking = false);
        TEntity FindById(string id, bool notracking = false);

        TEntity Insert(TEntity instance, bool withUnitCode = true);

        TEntity Update(TEntity instance,
            Action<TEntity, TEntity> updateAction = null,
            Func<TEntity, TEntity, bool> updateCondition = null);

        TEntity Delete(string id);
        TEntity AddUnit(TEntity instance);
    }
}
