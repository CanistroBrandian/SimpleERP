using Microsoft.EntityFrameworkCore;
using SimpleERP.Models.Abstract;
using SimpleERP.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Models.Repository
{
    public class CommonRepository<TEntity, TId> where TEntity : class, IEntity<TId>
    {

        readonly private ContextEF _context;
        CommonRepository(ContextEF context)
        {
            _context = context;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetSingleAsync(TId id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> FindAsync(TId id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> Update(TEntity model)
        {
            if (model != null)
            {
                _context.Set<TEntity>().Attach(model);
                _context.Entry(model).State = EntityState.Modified;
               await _context.SaveChangesAsync();
            }
            else throw new Exception("Значения модели не описаны");
            return model;
            }
        

        public async Task<TEntity> AddAsync(TEntity model)
        {
            if (model != null)
            {
                _context.Set<TEntity>().Attach(model);
                _context.Entry(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            else throw new Exception("Значения модели не описаны");
            return model;
        }
            
        public async Task<TEntity> Delete(TId id)
        {
            var dbEntry = _context.Set<TEntity>().FindAsync(id);
            if(dbEntry != null)
            {
                _context.Remove(dbEntry);
              await  _context.SaveChangesAsync();
            }
            return await dbEntry;
        }

    }
}
