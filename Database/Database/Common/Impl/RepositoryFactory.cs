using Database.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Common.Impl
{
    // <copyright file="Database.Common.Impl.RepositoryFactory">
    // Copyright (c) 2016 All Rights Reserved
    // <author>Manuel Lackenbucher</author>
    // <author>Thomas Huber</author>
    // </copyright>
    /// <summary>
    /// Factory for creating Repositores
    /// </summary>
    public class RepositoryFactory
    {

        private static RepositoryFactory instance = null;

        internal RepositoryFactory( ) { }

        public static RepositoryFactory Instance
        {
            get
            {
                if ( instance == null )
                {
                    instance = new RepositoryFactory();
                }
                return instance;
            }
        }


        public IRepository CreateRepository<T>() where T : IRepository
        {
            return Activator.CreateInstance<T>();
        }


    }
}
