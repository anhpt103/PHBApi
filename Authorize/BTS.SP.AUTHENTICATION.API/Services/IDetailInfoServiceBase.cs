using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BTS.SP.AUTHENTICATION.API;

namespace  BTS.SP.AUTHENTICATION.API.Services
{
    public interface IDetailInfoServiceBase<TEntity> : IEntityService<TEntity>
        where TEntity : DetailInfoEntity
    {
        new IDetailInfoServiceBase<TEntity> Include(Expression<Func<TEntity, object>> include);
        TEntity Find(TEntity instance, bool notracking = false);
        TEntity FindById(int id, bool notracking = false);

        TEntity Insert(TEntity instance);

        TEntity Update(TEntity instance,
            Action<TEntity, TEntity> updateAction = null,
            Func<TEntity, TEntity, bool> updateCondition = null);

        TEntity Delete(int id);
    }
}
