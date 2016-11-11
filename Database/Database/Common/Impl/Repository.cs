using Database.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Common.Impl
{
    // <copyright file="Database.Common.Impl">
    // Copyright (c) 2016 All Rights Reserved
    // <author>Manuel Lackenbucher</author>
    // </copyright>
    /// <summary>
    /// Repository class
    /// </summary>
    public class Repository : IRepository
    {
        public Repository( ) { }

        public void Delete<T>( T entity ) where T : IEntity
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

        public IQueryable<T> ToList<T>( ) where T : IEntity
        {
            throw new NotImplementedException();
        }
    }
}
