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
       
        IQueryable<T> SelectManyWhere<T>(DetachedCriteria criteria );
        IQueryable<T> SelectManyWhere<T>(DetachedCriteria criteria, params Order[] orders);
        IQueryable<T> SelectCertainNumberWhere<T>(DetachedCriteria criteria, int firstResult, int numberOfResults, params Order[] orders);
        T SelectSingle<T>(DetachedCriteria criteria);
        T SelectFirstOfMany<T>(DetachedCriteria criteria);
        T SelectFirstOfManyOrdered<T>(DetachedCriteria creteria, params Order[] orders);
        long CountWhere<T>(DetachedCriteria criteria);

        IQueryable<T> SelectMany<T>();
        T SelectSingleWhere<T>(Expression<Func<T, bool>> expression);
        IQueryable<T> SelectManyWhere<T>(Expression<Func<T, bool>> expression);

        void SaveOrUpdate<T>(T entity ) where T : IEntity;
        void SaveOrUpdateMore<T>(IEnumerable<T> items) where T : IEntity;
        void Delete<T>( T entity ) where T : IEntity;
        void DeleteWhere<T>(DetachedCriteria criteria);
        T GetById<T>( object objId ) where T : IEntity;

        IQueryable<T> ToList<T>( ) where T : IEntity;
       
    }
}
