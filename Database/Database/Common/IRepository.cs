using System;
using System.Collections.Generic;
using System.Linq;
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
        
        void Save<T>(T entity ) where T : IEntity;
        void Delete<T>( T entity ) where T : IEntity;
        T GetById<T>( object objId ) where T : IEntity;

        IQueryable<T> ToList<T>( ) where T : IEntity;
    }
}
