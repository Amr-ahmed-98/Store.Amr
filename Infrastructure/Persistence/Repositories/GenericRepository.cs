using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false)
        {
            return trackChanges ? 
                            await _context.Set<TEntity>().ToListAsync()
                          :
                            await _context.Set<TEntity>().AsNoTracking().ToListAsync();


            //if (trackChanges) return await _context.Set<TEntity>().ToListAsync();

            //return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetAsync(TKey id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }


        public async Task AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
        }
        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }
        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }
    }
}
