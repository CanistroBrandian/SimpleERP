using Microsoft.EntityFrameworkCore;
using SimpleERP.Models.Abstract;
using SimpleERP.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Repository
{
    public class CommonRepository<TEntity, TId> : ICommonRepository<TEntity,TId>
        where TEntity : class, IEntity<TId>
    {

        readonly private ContextEF _context;
        public CommonRepository(ContextEF context)
        {
            _context = context;
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public virtual async Task<TEntity> GetSingleAsync(TId id)
        {
            return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(s => s.Id.Equals(id));
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity model)
        {
            if (model == null) throw new Exception("Значения модели не описаны");

            _context.Set<TEntity>().Attach(model);
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }


        public virtual async Task<TEntity> AddAsync(TEntity model)
        {
            if (model == null) throw new Exception("Значения модели не описаны");

            _context.Set<TEntity>().Attach(model);
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }

        public virtual async Task<TEntity> DeleteAsync(TId id)
        {
            var dbEntry = await _context.Set<TEntity>().FirstOrDefaultAsync(s => s.Id.Equals(id));
            if (dbEntry == null) throw new Exception("Значения модели не описаны");

            _context.Remove(dbEntry);
            await _context.SaveChangesAsync();
            return dbEntry;
        }

    }
}
