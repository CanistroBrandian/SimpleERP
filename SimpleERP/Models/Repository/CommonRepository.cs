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

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetSingleAsync(TId id)
        {
            //Check if this method throws client side evaluation exception because of using of Equals method
            //return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(s => s.Id.ToString() == id.ToString());
            return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(s => s.Id.Equals(id));
        }

        public async Task<TEntity> UpdateAsync(TEntity model)
        {
            if (model == null) throw new Exception("Значения модели не описаны");

            _context.Set<TEntity>().Attach(model);
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }


        public async Task<TEntity> AddAsync(TEntity model)
        {
            if (model == null) throw new Exception("Значения модели не описаны");

            _context.Set<TEntity>().Attach(model);
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<TEntity> DeleteAsync(TId id)
        {
            var dbEntry = await _context.Set<TEntity>().FirstOrDefaultAsync(s => s.Id.Equals(id));
            if (dbEntry == null) throw new Exception("Значения модели не описаны");

            _context.Remove(dbEntry);
            await _context.SaveChangesAsync();
            return dbEntry;
        }

    }
}
