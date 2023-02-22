using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace e_Commerce.Data.Repository
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> All();
        IQueryable<TEntity> AllAsNoTracking();
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<int> SaveChangesAsync();
        public bool Exist(Expression<Func<TEntity, bool>> expression);
        public TEntity GetFirstBy(Expression<Func<TEntity, bool>> expression);
        public IQueryable<TEntity> GetAllBy(Expression<Func<TEntity, bool>> expression);
    }
}
