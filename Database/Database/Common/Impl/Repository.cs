using Database.Configuration;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Criterion;
using System.Linq.Expressions;

namespace Database.Common.Impl
{
    // <copyright file="Database.Common.Impl">
    // Copyright (c) 2016 All Rights Reserved
    // <author>Manuel Lackenbucher</author>
    // <author>Thomas Huber</author>
    // </copyright>
    /// <summary>
    /// Repository class
    /// </summary>
    public class Repository : IRepository
    {
        protected ISession Session = null;
        public Repository( ) { }

        /// <summary>
        /// Returns the number of entities matching the given criteria
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="creteria"></param>
        /// <returns>count as long</returns>
        public long CountWhere<T>(DetachedCriteria criteria)
        {
            try {
                return Convert.ToInt64(criteria.GetExecutableCriteria(Session)
                     .SetProjection(Projections.RowCountInt64()).UniqueResult());
            }
            catch(Exception ex)
            {
                throw (new DatabaseException(ex, "Error in counting the Rows", null));
            }
        }

        public void Delete<T>( T entity ) where T : IEntity
        {
            throw new NotImplementedException();
        }

        public void DeleteWhere<T>(DetachedCriteria creteria)
        {
            throw new NotImplementedException();
        }

        public T GetById<T>( object objId ) where T : IEntity
        {
            return Connection.Database.OpenSession().Get<T>(objId);
        }

        public void Save<T>( T entity ) where T : IEntity
        {
            throw new NotImplementedException();
        }

        public void SaveMore<T>(IEnumerable<T> items) where T : IEntity
        {
            //könnt ma noch brauchen vlt
            throw new NotImplementedException();
        }

        public IQueryable<T> Select<T>()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Selects all values of the given entity
        /// from the database which are matching 
        /// the given criteria.
        /// Should be used if a lot of data is expected!!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="creteria">the criteria the values are supposed to match</param>
        /// <returns></returns>
        public IQueryable<T> Select<T>(DetachedCriteria creteria)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> SelectCertainNumberWhere<T>(DetachedCriteria creteria, int firstResult, int numberOfResults, params Order[] orders)
        {
            throw new NotImplementedException();
        }

        public T SelectFirstOfMany<T>(DetachedCriteria creteria)
        {
            throw new NotImplementedException();
        }

        public T SelectFirstOfManyOrdered<T>(DetachedCriteria creteria, params Order[] orders)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> SelectMany<T>()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> SelectManyWhere<T>(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> SelectManyWhere<T>(DetachedCriteria creteria)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> SelectManyWhere<T>(DetachedCriteria creteria, params Order[] orders)
        {
            throw new NotImplementedException();
        }

        public T SelectSingle<T>(DetachedCriteria creteria)
        {
            throw new NotImplementedException();
        }

        public T SelectSingleWhere<T>(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> ToList<T>( ) where T : IEntity
        {
            throw new NotImplementedException();
        }
    }
}
