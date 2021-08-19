
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using Test_api.Utilities;

namespace Utilities
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal readonly DbContext _context;
        internal DbSet<TEntity> _dbSet;

        #region Generic Methods

        /// <summary>
        /// List of all entities in the context.
        /// </summary>
        public IList<TEntity> AllEntities
        {
            set { }
            get { return Entities.ToList(); }
        }

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// All entities in the context.
        /// </summary>
        private DbSet<TEntity> Entities
        {
            set { }
            get { return _dbSet ?? (_dbSet = _context.Set<TEntity>()); }
        }

        /// <summary>
        /// Provides a Get method with expressiion.
        /// </summary>
        /// <param name="filter">Expression to filter the result.</param>
        /// <param name="orderBy">Sorting function.</param>
        /// <param name="includeProperties">Properties to include in the result.</param>
        /// <returns>IEnumerable collection of TEntity types.</returns>
        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            try
            {
                IQueryable<TEntity> query = _dbSet;

                using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadUncommitted
                }))
                {
                    if (filter != null)
                    {
                        query = query.Where(filter);
                    }

                    query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
                }
                return orderBy != null ? orderBy(query) : query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Provides a Get method by Id.
        /// </summary>
        /// <param name="id">Unique Id of entity.</param>
        /// <returns>An object of TEntity type.</returns>
        public TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Provides an Insert method for TEntity object.
        /// </summary>
        /// <param name="entity">An object of TEntity type.</param>
        public virtual TEntity Insert(TEntity entity)
        {
            return _dbSet.Add(entity).Entity;
        }

        /// <summary>
        /// Provides a Delete method for TEntity object.
        /// </summary>
        /// <param name="id">Unique Id of entity.</param>
        public virtual void Delete(object id)
        {
            var entityToDelete = _dbSet.Find(id);

            Delete(entityToDelete);
        }

        /// <summary>
        /// Provides a Delete method for TEntity object.
        /// </summary>
        /// <param name="entityToDelete">Object of TEntity type to be deleted.</param>
        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }

            _dbSet.Remove(entityToDelete);
        }

        /// <summary>
        /// Provides an Update method for TEntity object.
        /// </summary>
        /// <param name="entityToUpdate">Object of TEntity type to be updated.</param>
        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        #endregion Generic Methods

        #region IDisposable Support

        private bool _disposed; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _context.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.

            // TODO: set large fields to null.
            Entities = null;
            AllEntities = null;

            _disposed = true;
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~GenericRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }
}



