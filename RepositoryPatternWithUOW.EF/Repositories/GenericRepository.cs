using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUOW.Core.Consts;
using RepositoryPatternWithUOW.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.EF.Repositories
{
 
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        protected ApplicationDbContext _context;

        public object Authors => throw new NotImplementedException();

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var all = await _context.Set<T>().ToListAsync();
            return all;
        }

        public async Task<T> FindAsync(Expression<Func<T,bool>> critaria, string[] includes = null)
        {

            IQueryable<T>query = _context.Set<T>();

            if(includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }
            var founded = await query.SingleOrDefaultAsync(critaria);
            return founded!;
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> critaria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }
            var founded = await query.Where(critaria).ToListAsync();
            return founded!;
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> critaria, int skip, int take)
        {
            return await _context.Set<T>().Where(critaria).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> critaria, int? skip, int? take, Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>().Where(critaria);

            if (take.HasValue)
                query = query.Take(take.Value);
            if (skip.HasValue)
                query = query.Skip(skip.Value);
            if(orderBy !=null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            return await query.ToListAsync();

        }

        public async Task<T> AddAsync(T entity)
        {
           await _context.Set<T>().AddAsync(entity);
           await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities;
        }
    }
}
