using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace MyPhamTrueLife.Common.DAL
{
    public interface IGenericRep<T> where T : class
    {
        #region -- Methods --

        /// <summary>
        /// Create the model
        /// </summary>
        /// <param name="m">The model</param>

        void Create(T m);

        /// <summary>
        /// Create list model
        /// </summary>
        /// <param name="l">List  model</param>

        void Create(List<T> l);

        /// <summary>
        /// Read by
        /// </summary>
        /// <param name="p">Predicate</param>
        /// <returns>Return query data</returns>

        IQueryable<T> Read(Expression<Func<T, bool>> p);

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Prinary key</param>
        /// <returns>Return the object</returns>

        T Read(int id);

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="code">Secondary key</param>
        /// <returns>Return the object</returns>

        T Read(string code);

        /// <summary>
        /// Update the model
        /// </summary>
        /// <param name="m">The model</param>

        void Update(T m);

        /// <summary>
        /// Update list model
        /// </summary>
        /// <param name="l">List model</param>

        #endregion

        #region -- Propperties --

        /// <summary>
        /// Return query all data
        /// </summary>

        IQueryable<T> All { get; }

        void Update<T>(List<T> l) where T : class;

        #endregion
    }
}
