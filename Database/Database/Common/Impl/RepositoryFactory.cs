// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-11</date>

using Database.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Common.Impl
{
   
    /// <summary>
    /// Factory for creating Repositores
    /// </summary>
    public class RepositoryFactory
    {
        /// <summary>
        /// The instance
        /// </summary>
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

        /// <summary>
        /// Factory method for creating a repository
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IRepository CreateRepository<T>() where T : IRepository
        {
            return (T)Activator.CreateInstance(typeof(T),true);
        }


    }
}
