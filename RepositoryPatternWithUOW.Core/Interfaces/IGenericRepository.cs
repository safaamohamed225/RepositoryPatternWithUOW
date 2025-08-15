using RepositoryPatternWithUOW.Core.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.Interfaces
{
    public interface IGenericRepository<T> where T :class
    {
        object Authors { get; }

        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> FindAsync(Expression<Func<T,bool>> critaria, string[] includes = null);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> critaria, string[] includes = null);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> critaria, int skip, int take);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> critaria, int? skip, int? take,
            Expression<Func<T,object>> orderBy= null, string orderByDirection = OrderBy.Ascending);

        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);



    }
}
