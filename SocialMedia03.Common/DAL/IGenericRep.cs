using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia03.Common.DAL
{
    public interface IGenericRep<T> where T : class
    {
        #region -- Methods --

        /// <summary>
        /// Create the model
        /// </summary>
        /// <param name="m">The model</param>
        bool Create(T m);

        /// <summary>
        /// Create list model
        /// </summary>
        /// <param name="l">List model</param>
        bool Create(List<T> l);

        /// <summary>
        /// Read by
        /// </summary>
        /// <param name="p">Predicate</param>
        /// <returns>Return query data</returns>
        IQueryable<T> Get<T>(Expression<Func<T, bool>> p) where T : class;

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        T GetSingle<T>(int id) where T : TEntity;

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="code">Secondary key</param>
        /// <returns>Return the object</returns>
        T Get(string code);

        /// <summary>
        /// Update the model
        /// </summary>
        /// <param name="m">The model</param>
        bool Update(T m);

        /// <summary>
        /// Update list model
        /// </summary>
        /// <param name="l">List model</param>
        bool Update(List<T> l);

        /// <summary>
        /// Delete the model
        /// </summary>
        /// <param name="m">The model</param>
        /// <returns>Return the object</returns>
        bool Delete(T m);

        #endregion

        #region -- Properties --

        /// <summary>
        /// Return query all data
        /// </summary>
        IQueryable<T> All { get; }

        #endregion
    }
}
