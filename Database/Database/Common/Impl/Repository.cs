// <copyright file="Database.Common.Impl.repository.cs">
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-11</date>
// </copyright>

using Database.Configuration;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Criterion;
using System.Linq.Expressions;
using NHibernate.Linq;
using System.Reflection;
using Verwaltung.Exception;
using System.Data.SqlClient;

namespace Database.Common.Impl
{
    /// <summary>
    /// Repository class
    /// </summary>

    //wie werma den das mitn session close machn?? 
    //lass ma de imma offen oda machma imma a neue oda ka?
    public class Repository : IRepository
    {
        #region fields
        #region protected fields
        protected ISession session = null;
        protected ITransaction Transaction = null;
        #endregion protected fields
        #endregion fields

        #region constructors
        public Repository( ISession _session ) { session = _session; }
        public Repository( ) { session = Database.Connection.Database.Instance.OpenSession(); }
        #endregion constructors

        #region transaction session methods
        /// <summary>
        /// commits the transaction and 
        /// closes it
        /// </summary>
        public void CommitTransaction( )
        {


            try
            {
                Transaction.Commit();
                CloseTransaction();
            }
            catch ( DatabaseException ex )
            {
                throw;
            }
            catch ( Exception ex )
            {

                throw ( new DatabaseException(ex , "Error at trancaction commit") );
            }

        }

        /// <summary>
        /// Rollbacks the transaction
        /// and closes it.
        /// Closes the session!
        /// </summary>
        public void RollbackTransaction( )
        {

            try
            {
                Transaction.Rollback();
                CloseTransaction();
            }
            catch ( DatabaseException ex )
            {
                throw;
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Errow at transaction rollback") );
            }


        }

        /// <summary>
        /// Disposes the Transaction
        /// and sets it to null
        /// </summary>
        private void CloseTransaction( )
        {

            try
            {
                Transaction.Dispose();
                Transaction = null;
            }
            catch ( DatabaseException )
            {
                throw;
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Error at closing the transaction") );
            }


        }

        

        private void CloseSession( )
        {

            try
            {
                session.Close();
                session.Dispose();
                session = null;
            }
            catch ( DatabaseException )
            {
                throw;
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Error at closing the session") );
            }

        }
        #endregion

        #region other methods like save/update delete ...

        /// <summary>
        /// creates an sql query and returns it
        /// </summary>
        /// <param name="query">the query string</param>
        /// <returns>the sql query</returns>
        public ISQLQuery GetQuery( string query )
        {
            return this.session.CreateSQLQuery(query);
        }



        /// <summary>
        /// Returns the number of entities matching the given criteria
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="creteria"></param>
        /// <returns>count as long</returns>
        public long CountWhere<T>( DetachedCriteria criteria ) where T : IEntity
        {
            try
            {
                return Convert.ToInt64(criteria.GetExecutableCriteria(session)
                     .SetProjection(Projections.RowCountInt64()).UniqueResult());
            }
            catch ( DatabaseException ex )
            {
                throw;
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Error in counting the Rows" , null) );
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
                Transaction = session.BeginTransaction();
                session.Delete(entity);
                CommitTransaction();
            }
            catch ( DatabaseException ex )
            {
                throw;
            }
            catch ( Exception ex )
            {
                RollbackTransaction();
                throw ( new DatabaseException(ex , "Could not delete the entity!" , entity) );
            }
        }

        /// <summary>
        /// calls the delete method foreach entity matching the
        /// given criteria
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="criteria"></param>
        public void DeleteWhere<T>( DetachedCriteria criteria ) where T : IEntity
        {
            try
            {
                SelectManyWhere<T>(criteria).ToList<T>().
                    ForEach(item => Delete(item));
            }
            catch ( DatabaseException dex )
            {
                throw ( dex );
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Error in deletewhere!") );
            }
        }

        /// <summary>
        /// Return the maximum of a column
        /// </summary>
        /// <typeparam name="T">The Enitity type representing the database table</typeparam>
        /// <typeparam name="E">The type of the column</typeparam>
        /// <param name="propertyName">The property name</param>
        /// <returns></returns>
        public E Max<T, E>( string propertyName ) where T : IEntity
        {
            try
            {
                return DetachedCriteria.For<T>().SetProjection(
                   Projections.Max(propertyName))
                   .GetExecutableCriteria(session).UniqueResult<E>();
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Error in selecting max") );
            }

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
                return Connection.Database.Instance.OpenSession().Get<T>(objId);
            }
            catch ( DatabaseException )
            {
                throw;
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Could not load the entity with the id " + objId , null) );
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
                Transaction = session.BeginTransaction();
                session.SaveOrUpdate(entity);
                CommitTransaction();
            }
            catch ( DatabaseException )
            {
                throw;
            }
            catch ( Exception ex )
            {
                RollbackTransaction();
                throw ( new DatabaseException(ex , "Could not save/update the entity!" , entity) );
            }
        }

        /// <summary>
        /// Calles the save or update method foreach entity in the List
        /// Throws an Exception if an error occurs
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        public void SaveOrUpdateMore<T>( IEnumerable<T> items ) where T : IEntity
        {
            try
            {
                items.ToList().ForEach(x => SaveOrUpdate<T>(x));
            }
            catch ( DatabaseException dex )
            {
                throw ( dex );
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "An Error in List occurd") );
            }

        }
        #endregion

        #region Criteria methods

        /// <summary>
        /// selects a certain amount of entitys 
        /// matching a given criteria.
        /// Should be used if a lot of data is expected!!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="criteria"></param>
        /// <param name="firstResult">the index of the first result</param>
        /// <param name="numberOfResults">how many results do you want to have</param>
        /// <param name="orders">the order in which it should be selected</param>
        /// <returns></returns>
        public IEnumerable<T> SelectCertainNumberWhere<T>( DetachedCriteria criteria , int firstResult , int numberOfResults , params Order[] orders ) where T : IEntity
        {
            try
            {
                criteria.SetFirstResult(firstResult).SetMaxResults(numberOfResults);
                return SelectManyWhere<T>(criteria , orders);
            }
            catch ( DatabaseException dex )
            {
                throw ( dex );
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Different Error in selecting") );
            }
        }

        /// <summary>
        /// Retunrs the First result matching the given
        /// criteria or a default entity if there is no result
        /// thorws an exception if an error occurs.
        /// Should be used if a lot of data is expected!!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public T SelectFirstOfMany<T>( DetachedCriteria criteria ) where T : IEntity
        {
            try
            {
                var results = criteria.SetFirstResult(0).SetMaxResults(1)
                                .GetExecutableCriteria(session).List<T>();
                return ( results.Count > 0 ) ? results[0] : default(T);
            }
            catch ( DatabaseException dex )
            {
                throw ( dex );
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Could not select first entity!") );
            }
        }

        /// <summary>
        /// returns the first result matching the criteria given
        /// and ordered in the given order.
        /// Throws an Exception if an error occurs.
        ///  Should be used if a lot of data is expected!!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="criteria"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public T SelectFirstOfManyOrdered<T>( DetachedCriteria criteria , Order order ) where T : IEntity
        {
            try
            {
                return SelectFirstOfMany<T>(criteria.AddOrder(order));
            }
            catch ( DatabaseException dex )
            {
                throw ( dex );
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Could not select first entity orderd!" , null) );
            }
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
        public IEnumerable<T> SelectManyWhere<T>( DetachedCriteria criteria ) where T : IEntity
        {
            try
            {
                return criteria.GetExecutableCriteria(session).List<T>();
            }
            catch ( DatabaseException dex )
            {
                throw ( dex );
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Could not select all entity!" , "selectManyWhere") );
            }
        }

        /// <summary>
        /// returns all entitys matching the given criteria
        /// and orders the result in the given order.
        /// Throws an exception if an error occurs.
        /// Should only be used if a lot of data is expected!!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="criteria"></param>
        /// <param name="orders"></param>
        /// <returns></returns>
        public IEnumerable<T> SelectManyWhere<T>(
            DetachedCriteria criteria , params Order[] orders ) where T : IEntity
        {
            try
            {

                orders?.ToList().ForEach(item => criteria.AddOrder(item));

                return SelectManyWhere<T>(criteria);
            }
            catch ( DatabaseException dex )
            {
                throw ( dex );
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Could not select all entity!" , "selectManyWhere") );
            }

        }

        /// <summary>
        /// returns the one entity matching the criteria.
        /// Throws an exception if more than entity matching the criteria.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="creteria"></param>
        /// <returns></returns>
        public T SelectSingle<T>( DetachedCriteria criteria ) where T : IEntity
        {
            try
            {
                return criteria.GetExecutableCriteria(session).UniqueResult<T>();
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "More than one entity is matching the criteria" , "SelectSingel") );
            }
        }
        #endregion

        #region Linq methods

        /// <summary>
        /// returns a linq queryable list of all entities.
        /// Throws an exception if an error occurs.
        /// Should be used if not much data is expeced.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IQueryable<T> SelectMany<T>( )
        {
            try
            {
                return session.Linq<T>();
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Could not select entity/ies. Watch your linq expression!" , "selectManyWhere Linq Expression") );
            }
        }

        /// <summary>
        /// calls the selectMany method and quries the result with the
        /// given linq expression!
        /// Throws an exception if an error occurs.
        /// Should be used if not much data is expeced.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">the linq expression you want to query the results</param>
        /// <returns>a IQueryable of the entíty T</returns>
        public IQueryable<T> SelectManyWhere<T>(
            Expression<Func<T , bool>> expression ) where T : IEntity
        {
            try
            {
                return session.Query<T>().Where(expression);
            }
            catch ( DatabaseException dex )
            {
                throw ( dex );
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Could not select entities. Watch your linq expression!" , "selectManyWhere Linq Expression") );
            }
        }

        /// <summary>
        /// Calls the SelectManyWhere method with the given linq expression.
        /// returns only one entity matching the expression!
        /// Throws an excpetion if there are more entities matching the expression
        /// or if other errors occur.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns>a singel entity or an exception if more entities matching the expression</returns>
        public T SelectSingleWhere<T>(
            Expression<Func<T , bool>> expression ) where T : IEntity
        {
            try
            {
                return SelectManyWhere(expression).Single();
            }
            catch ( DatabaseException dex )
            {
                throw ( dex );
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Could not select entity. Watch your linq expression!" , "selectSingelWhere Linq Expression") );
            }
        }
        #endregion


        /// <summary>
        /// commits open transactions if there are any.
        /// flushes the session and closes it.
        /// </summary>
        public void Dispose( )
        {
            if ( Transaction != null )
            {
                CommitTransaction();
            }
            if ( session != null )
            {
                session.Flush();
                CloseSession();
            }
        }
    }
}
