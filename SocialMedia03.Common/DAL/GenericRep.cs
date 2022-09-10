using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia03.Common.DAL
{
    public class GenericRep<C, T> : IGenericRep<T> where T : class where C : DbContext, new()
    {
        #region -- Implements --

        /// <summary>
        /// Create the model
        /// </summary>
        /// <param name="m">The model</param>
        public bool Create(T m)
        {
            try
            {
                _context.Set<T>().Add(m);
                _context.SaveChanges();

                return true;
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// Create list model
        /// </summary>
        /// <param name="l">List model</param>
        public bool Create(List<T> l)
        {  
            try
            {
            _context.Set<T>().AddRange(l);
            _context.SaveChanges();
                return true;
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// Read by
        /// </summary>
        /// <param name="p">Predicate</param>
        /// <returns>Return query data</returns>
        public IQueryable<T> Get<T>(Expression<Func<T, bool>> p) where T : class
        {
            return _context.Set<T>().Where(p);
        }

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public virtual T GetSingle<T>(int id) where T: TEntity
        {
            return _context.Set<T>().Where(T => T.Id == id).SingleOrDefault();
        }

        public virtual T GetSingle<T>(Expression<Func<T, bool>> p) where T : class
        {
            return _context.Set<T>().Where(p).SingleOrDefault();
        }

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="code">Secondary key</param>
        /// <returns>Return the object</returns>
        public virtual T Get(string code)
        {
            return null;
        }

        /// <summary>
        /// Update the model
        /// </summary>
        /// <param name="m">The model</param>
        public bool Update(T m)
        {
            try
            {
                _context.Set<T>().Update(m);
                _context.SaveChanges();

                return true;
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// Update list model
        /// </summary>
        /// <param name="l">List model</param>
        public bool Update(List<T> l)
        {
            try
            {
                _context.Set<T>().UpdateRange(l);
                _context.SaveChanges();

                return true;
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// Return query all data
        /// </summary>
        public IQueryable<T> All
        {
            get
            {
                return _context.Set<T>();
            }
        }

        #endregion

        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public GenericRep()
        {
            _context = new C();
        }

        /// <summary>
        /// Update the model
        /// </summary>
        /// <param name="old">The old model</param>
        /// <param name="new">The new model</param>
        protected object Update(T old, T @new)
        {
            _context.Entry(old).State = EntityState.Modified;
            var res = _context.Set<T>().Add(@new);
            return res;
        }

        /// <summary>
        /// Delete the model
        /// </summary>
        /// <param name="m">The model</param>
        /// <returns>Return the object</returns>
        public bool Delete(T m)
        {
            if (m == null)
                return true;
            try
            {
                var t = _context.Set<T>().Remove(m);

                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                if (e is DbUpdateException)
                    return true;
                Debug.WriteLine(e.StackTrace);
                return false;
            }
        }

        #endregion

        #region -- Properties --

        /// <summary>
        /// The database context
        /// </summary>
        public C Context
        {
            get { return _context; }
            set { _context = value; }
        }

        #endregion

        #region -- Fields --

        /// <summary>
        /// The entities
        /// </summary>
        private C _context;

        #endregion
    }
}