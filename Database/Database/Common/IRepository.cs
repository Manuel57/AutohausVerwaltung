using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Database.Common
{
    // <copyright file="Databast.Common.IRepository">
    // Copyright (c) 2016 All Rights Reserved
    // <author>Manuel Lackenbucher</author>
    // <author>Thomas Huber</author>
    // </copyright>
    /// <summary>
    /// Repository interface
    /// </summary>
    public interface IRepository
    {
        //Criteria methods
        IEnumerable<T> SelectManyWhere<T>(DetachedCriteria criteria ) where T : IEntity;
        IEnumerable<T> SelectManyWhere<T>(DetachedCriteria criteria, params Order[] orders) where T : IEntity;
        IEnumerable<T> SelectCertainNumberWhere<T>(DetachedCriteria criteria, int firstResult, int numberOfResults, params Order[] orders) where T : IEntity;
        T SelectSingle<T>(DetachedCriteria criteria) where T : IEntity;
        T SelectFirstOfMany<T>(DetachedCriteria criteria) where T : IEntity;
        T SelectFirstOfManyOrdered<T>(DetachedCriteria creteria,  Order order) where T : IEntity;
        long CountWhere<T>(DetachedCriteria criteria) where T : IEntity;

        //Linq methods
        IQueryable<T> SelectMany<T>();
        T SelectSingleWhere<T>(Expression<Func<T, bool>> expression) where T : IEntity;
        IQueryable<T> SelectManyWhere<T>(Expression<Func<T, bool>> expression) where T : IEntity;

        //Other mehods like save/update delete usw
        void SaveOrUpdate<T>(T entity ) where T : IEntity;
        void SaveOrUpdateMore<T>(IEnumerable<T> items) where T : IEntity;
        void Delete<T>( T entity ) where T : IEntity;
        void DeleteWhere<T>(DetachedCriteria criteria) where T : IEntity;
        T GetById<T>( object objId ) where T : IEntity;

        IQueryable<T> ToList<T>( ) where T : IEntity;
       
    }
}
