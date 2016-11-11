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
        protected ITransaction Transaction = null;
        public Repository( ) { }

        #region transaction methods
        /// <summary>
        /// commits the transaction and 
        /// closes it
        /// </summary>
        public void CommitTransaction()
        {
            Transaction.Commit();
            CloseTransaction();
        }
        
        /// <summary>
        /// Rollbacks the transaction
        /// and closes it
        /// </summary>
        public void RollbackTransaction()
        {
            Transaction.Rollback();
            CloseTransaction();

        }

        /// <summary>
        /// Disposes the Transaction
        /// and sets it to null
        /// </summary>
        private void CloseTransaction()
        {
            Transaction.Dispose();
            Transaction = null;
        }
        #endregion
       
        
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

        /// <summary>
        /// Deletes the given Entity and commits it
        /// If an error occurs:
        /// 1.Rollback is called
        /// 2.Excetpion gets thrown
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public virtual void Delete<T>( T entity ) where T : IEntity
        {
            try
            {
                Transaction = Session.BeginTransaction();
                Session.Delete(entity);
                CommitTransaction();
            }
            catch(Exception ex)
            {
                RollbackTransaction();
                throw (new DatabaseException(ex, "Could not delete the entity!", entity));
            }
        }

        public void DeleteWhere<T>(DetachedCriteria creteria)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Loads the Entity with the given id from the database
        /// throws an exception if an error occurs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objId">the id of the entity to load</param>
        /// <returns>the entity or null if it doesn't exist</returns>
        public T GetById<T>( object objId ) where T : IEntity
        {
            try
            {
                return Connection.Database.OpenSession().Get<T>(objId);
            }
            catch(Exception ex)
            {
                throw (new DatabaseException(ex, "Could not load the entity with the id " + objId, null));
            }
           
        }

        /// <summary>
        /// Saves or updates the given entity
        /// throws an exception if an error occurs
        /// and does a rollback!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public void SaveOrUpdate<T>( T entity ) where T : IEntity
        {
            try
            {
                Transaction = Session.BeginTransaction();
                Session.SaveOrUpdate(entity);
                CommitTransaction();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw (new DatabaseException(ex, "Could not save/update the entity!", entity));
            }
        }

        /// <summary>
        /// Calles the save or update method foreach entity in the List
        /// Throws an Exception if an error occurs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        public void SaveOrUpdateMore<T>(IEnumerable<T> items) where T : IEntity
        {
            try
            {
                foreach(T item in items)
                {
                    SaveOrUpdate(item);
                }
            }
            catch (DatabaseException dex)
            {
                throw (dex);
            }
            catch (Exception ex)
            {
                throw (new DatabaseException(ex, "An Error in List occurd"));
            }
            
        }

        #region Criteria methods
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


        /// <summary>
        /// Selects all values of the given entity
        /// from the database which are matching 
        /// the given criteria.
        /// Should be used if a lot of data is expected!!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="creteria">the criteria the values are supposed to match</param>
        /// <returns></returns>
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
        #endregion

        #region Linq methods
        public IQueryable<T> SelectMany<T>()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> SelectManyWhere<T>(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public T SelectSingleWhere<T>(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }
        #endregion

        public IQueryable<T> ToList<T>( ) where T : IEntity
        {
            throw new NotImplementedException();
        }

       
    }
}
