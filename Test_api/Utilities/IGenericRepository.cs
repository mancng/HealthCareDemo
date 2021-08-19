using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Test_api.Utilities
{
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : class
    {
        #region Generic Methods

        /// <summary>
        /// Provides a Get method with expressiion.
        /// </summary>
        /// <param name="filter">Expression to filter the result.</param>
        /// <param name="orderBy">Sorting function.</param>
        /// <param name="includeProperties">Properties to include in the result.</param>
        /// <returns>IEnumerable collection of TEntity types.</returns>
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        /// <summary>
        /// Provides a Get method by Id.
        /// </summary>
        /// <param name="id">Unique Id of entity.</param>
        /// <returns>An object of TEntity type.</returns>
        TEntity GetById(object id);

        /// <summary>
        /// Provides an Insert method for TEntity object.
        /// </summary>
        /// <param name="entity">An object of TEntity type.</param>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Provides a Delete method for TEntity object.
        /// </summary>
        /// <param name="id">Unique Id of entity.</param>
        void Delete(object id);

        /// <summary>
        /// Provides a Delete method for TEntity object.
        /// </summary>
        /// <param name="entityToDelete">Object of TEntity type to be deleted.</param>
        void Delete(TEntity entityToDelete);

        /// <summary>
        /// Provides an Update method for TEntity object.
        /// </summary>
        /// <param name="entityToUpdate">Object of TEntity type to be updated.</param>
        void Update(TEntity entityToUpdate);
        void Save();

        #endregion


    }
}
