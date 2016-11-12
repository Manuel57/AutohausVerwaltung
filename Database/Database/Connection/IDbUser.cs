// <copyright file="Database.Connection.idbuser.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-11</date>
// </copyright>

namespace Database.Connection
{
    /// <summary>
    /// interface representing a database user
    /// </summary>
    public interface IDbUser
    {
        string Username { get;}
        string Password { get;  }
     
    }
}