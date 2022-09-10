using SocialMedia03.Common.DAL;
using SocialMedia03.Common.Res;
using System.Linq.Expressions;

namespace SocialMedia03.Common.BLL
{
    public class GenericSvc<D, T> : IGenericSvc<T> where T : class where D : IGenericRep<T>, new()
    {
        #region -- Implements --

        /// <summary>
        /// Create the model
        /// </summary>
        /// <param name="m">The model</param>
        /// <returns>Return the result</returns>
        public virtual SingleRes Create(T m)
        {
            var res = new SingleRes();

            try
            {
                var now = DateTime.Now;
                _rep.Create(m);
            }
            catch (Exception ex)
            {
                res.SetError(ex.StackTrace);
            }

            return res;
        }


        /// <summary>
        /// Read by
        /// </summary>
        /// <param name="p">Predicate</param>
        /// <returns>Return query data</returns>
        public IQueryable<T> Get(Expression<Func<T, bool>> p)
        {
            return _rep.Get(p);
        }


        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public virtual T Get<T>(int id) where T: TEntity
        {
            return _rep.GetSingle<T>(id);
        }

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="code">Secondary key</param>
        /// <returns>Return the object</returns>
        public virtual SingleRes Get(string code)
        {
            return null;
        }

        /// <summary>
        /// Update the model
        /// </summary>
        /// <param name="m">The model</param>
        /// <returns>Return the result</returns>
        public virtual SingleRes Update(T m)
        {
            var res = new SingleRes();

            try
            {
                _rep.Update(m);
            }
            catch (Exception ex)
            {
                res.SetError(ex.StackTrace);
            }

            return res;
        }



        /// <summary>
        /// Delete single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the result</returns>
        public virtual bool Delete(T obj)
        {
            return _rep.Delete(obj);
        }

        /// <summary>
        /// Delete single object
        /// </summary>
        /// <param name="code">Secondary key</param>
        /// <returns>Return the result</returns>
        public virtual SingleRes Delete(string code)
        {
            return null;
        }

        /// <summary>
        /// Restore the model
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the result</returns>
        public virtual SingleRes Restore(int id)
        {
            return null;
        }

        /// <summary>
        /// Restore the model
        /// </summary>
        /// <param name="code">Secondary key</param>
        /// <returns>Return the result</returns>
        public virtual SingleRes Restore(string code)
        {
            return null;
        }

        /// <summary>
        /// Remove and not restore
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Number of affect</returns>
        public virtual int Remove(int id)
        {
            return 0;
        }

        /// <summary>
        /// Return query all data
        /// </summary>
        public IQueryable<T> All
        {
            get
            {
                return _rep.All;
            }
        }

        #endregion

        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public GenericSvc()
        {
            _rep = new D();
        }

        #endregion

        #region -- Fields --

        /// <summary>
        /// The repository
        /// </summary>
        protected D _rep;

        #endregion
    }
}
