using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Abstract
{
    public interface ICommonRepository<TEntity, TId>
    {
         Task<List<TEntity>> GetAllAsync();
         Task<TEntity> GetSingleAsync(TId id);
         Task<TEntity> UpdateAsync(TEntity model);
         Task<TEntity> AddAsync(TEntity model);
         Task<TEntity> DeleteAsync(TId id);

    }
}
