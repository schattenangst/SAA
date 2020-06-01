
namespace ApiAuthorizationAA.Persistence
{
    using Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class BasePersistence<T> : IBasePersistence<T> where T : class
    {
        #region Fields
        protected DbSet<T> DbSet;
        protected readonly IRepositoryContext repositoryContext;
        private DbContext dbContext;
        #endregion

        #region Constructor
        /// <summary>
        /// Inicializando interfaces
        /// </summary>
        /// <param name="context"></param>
        public BasePersistence(IRepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
            dbContext = repositoryContext as ApplicationDbContext;
            dbContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }
        #endregion

        #region Properties
        #endregion

        #region Methods Public

        /// <summary>
        /// Counts the number of records that match a search
        /// </summary>
        /// <param name="filter">
        ///     Funtion to filter
        /// </param>
        /// <param name="navigationProperties">
        ///     Properties of navigation
        /// </param>
        /// <returns></returns>
        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] navigationProperties)
        {
            int counter = 0;
            IQueryable<T> query = GetQueryable();

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
            {
                query = query.Include(navigationProperty);
            }

            query = query.Where(filter);
            counter = await query.CountAsync();
            return counter;
        }

        /// <summary>
        /// Insert records multiples
        /// </summary>
        /// <param name="entities">
        ///     Collection will be inserted into database
        /// </param>
        /// <returns>
        ///     Return object whith data
        /// </returns>
        public virtual async Task<IEnumerable<T>> Create(IEnumerable<T> entities)
        {
            var dbSet = dbContext.Set<T>();
            dbSet.AddRange(entities);

            return await dbContext.SaveChangesAsync() == 0 ? null : entities;
        }

        /// <summary>
        /// Create a new register
        /// </summary>
        /// <param name="entity">
        ///     Object will be insert into data base
        /// </param>
        /// <returns>
        ///     Return a object with inserted data
        /// </returns>
        public virtual async Task<T> Create(T entity)
        {
            dbContext.Set<T>().Add(entity);
            var result = await dbContext.SaveChangesAsync();
            return result == 0 ? null : entity;
        }

        /// <summary>
        /// Edit a exist register
        /// </summary>
        /// <param name="entity">
        ///     Object will be updated into database
        /// </param>
        /// <returns>
        ///     Return a object with data updated
        /// </returns>
        public virtual async Task<T> Edit(T entity)
        {
            var modifyEntity = dbContext.Entry(entity);
            DbSet = dbContext.Set<T>();
            DbSet.Attach(entity);
            modifyEntity.State = EntityState.Modified;

            var result = await dbContext.SaveChangesAsync();
            return result == 0 ? null : entity;
        }

        public virtual async Task<bool> Edit(IEnumerable<T> entities)
        {
            bool status = false;

            foreach (var entity in entities)
            {
                var modifyEntity = dbContext.Entry(entities);
                DbSet = dbContext.Set<T>();

                DbSet.Attach(entity);
                modifyEntity.State = EntityState.Modified;
                var result = await dbContext.SaveChangesAsync();

                status = true;
            }

            return status;
        }

        /// <summary>
        /// Remove a register
        /// </summary>
        /// <param name="entity">
        ///     Object will be remove/delete from database
        /// </param>
        /// <returns></returns>
        public virtual async Task<int> Remove(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Deleted;
            var result = await dbContext.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        public virtual async Task<ICollection<T>> FindAsync(Expression<Func<T, bool>> filtro, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = GetQueryable();

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
            {
                query = query.Include(navigationProperty);
            }

            return await query.Where(filtro).ToListAsync();
        }

        /// <summary>
        /// Get the first record of a table from a filter
        /// </summary>
        /// <param name="filter">
        ///     Filter function
        /// </param>
        /// <param name="navigationProperties">
        ///     Navigation properties
        /// </param>
        /// <returns></returns>
        public virtual async Task<T> FindFirstAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = GetQueryable();

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
            {
                query = query.Include(navigationProperty);
            }

            return await query.FirstOrDefaultAsync(filter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual async Task<ICollection<T>> FindAllAsync()
        {
            IQueryable<T> query = GetQueryable();

            return await query.ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        public virtual async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>>[] filters = null, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = GetQueryable();

            return await query.ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        public virtual async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = GetQueryable();

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
            {
                query = query.Include(navigationProperty);
            }

            return await query.Where(filter).ToListAsync();
        }

        /// <summary>
        /// Dispose object <paramref name="dbContext"/>
        /// </summary>
        public void Dispose()
        {
            dbContext?.Dispose();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IQueryable<T> GetQueryable()
        {
            return dbContext.Set<T>();
        }
    }
}
