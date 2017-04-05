// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-11</date>

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