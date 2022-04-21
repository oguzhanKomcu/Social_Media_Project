using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SMP.Domain.Models.Interface;
using SMP.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMP.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {

        private readonly AppDbContext _appDbContext;
        private readonly DbSet<T> table;

        protected BaseRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            this.table = appDbContext.Set<T>();
        }

        public async Task<bool> Any(Expression<Func<T, bool>> expression)
        {
            return await table.AnyAsync(expression);
        }

        public async Task Create(T entity)
        {
            await table.AddAsync(entity);
        }


        public void Delete(T entity) 
        {
            
        }

        public async Task<T> GetDefault(Expression<Func<T, bool>> expression)
        {
            return await table.FirstOrDefaultAsync(expression);
        }

        public async Task<TResult> GetFilteredFirstOrDefault<TResult>(Expression<Func<T, TResult>> selector,
                                                                 Expression<Func<T, bool>> expression,
                                                                Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool dissableTracing = true)



        {
            IQueryable<T> query = table;
            if (dissableTracing)
            {
                query = query.AsNoTracking(); 
            }

            if (include != null)
            {
                query = include(query);
            }

            if (expression != null)
            {
                query = query.Where(expression);
            }


         
            return await query.Select(selector).FirstOrDefaultAsync();

        }

        public async Task<List<TResult>> GetFilteredList<TResult>(Expression<Func<T, TResult>> selector,
                                                  Expression<Func<T, bool>> expression,
                                                   Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                    Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                    bool dissableTracing = true,
                                    int pageIndex = 1,
                                    int pageSize = 3)
        {
            IQueryable<T> query = table;
            if (dissableTracing) 
            {
                query = query.AsNoTracking(); 
            }

            if (include != null)
            {
                query = include(query);
            }

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (orderBy != null)
            {
                return await orderBy(query).Select(selector).Skip((pageIndex - 1) * pageSize).ToListAsync();
            }
            else
            {
                return await query.Select(selector).Skip((pageIndex - 1) * pageSize).ToListAsync();

            }

        }

        public void Update(T entity)
        {
            _appDbContext.Entry<T>(entity).State = EntityState.Modified;
        }
    }
}
