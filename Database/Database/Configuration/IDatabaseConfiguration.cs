// <copyright file="Database.Configuration.IDatabaseConfiguration.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-13</date>
// </copyright>

using System;
using System.Net;
using System.Reflection;
using Database.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace Database.Configuration
{
    public interface IDatabaseConfiguration
    {
        Assembly assembly { get; set; }
        Type Dialect { get; }
        Type Driver { get; }

        Assembly GetAssembly( );
        void RegisterAll( string provider , IPAddress dataSource , string service , IDbUser user , Type dialect , Type driver , Assembly assembly );
        void RegisterAssembly( Assembly assembly );
        void RegisterDataSource( IPAddress dataSource );
        void RegisterDialect<T>( ) where T : Dialect;
        void RegisterDriver<T>( ) where T : IDriver;
        void RegisterProvider( string provider );
        void RegisterServicename( string servicename );
        void RegisterUser( IDbUser user );
        void Updated( );
    }
}