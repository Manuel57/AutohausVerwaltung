using Database.Common;
using Database.Common.Impl;
using LagerverwaltungBL.Model;
using Newtonsoft.Json;
using NHibernate;
using NHibernate.Criterion;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verwaltung.Exception;

namespace LagerverwaltungBL.Controller
{
    public class SdoManager
    {
        private static IRepository repository = null;


        /// <summary>
        /// Creates a json string containing the coordinates of all zentrallager having the given teil instock
        /// </summary>
        /// <param name="teil">the teil</param>
        /// <param name="werkstat">the werkstatt</param>
        /// <returns>json string</returns>
        public static string GetJsonCoordinates( Autoteile teil ,string werkstat)
        {
            string ret = string.Empty;
            ret = GetJsonCoordinates(GetZentrallagerByTeil(teil), werkstat);
            return ret;
        }
        /// <summary>
        /// Gets all Zentrallager having the given teil
        /// </summary>
        /// <param name="teil">the teil</param>
        /// <returns>List of <see cref="LagerverwaltungBL.Model.Zentrallager"/></returns>
        public static List<Zentrallager> GetZentrallagerByTeil( Autoteile teil )
        {
            try
            {
                List<Zentrallager> lager = null;
                using ( repository = RepositoryFactory.Instance.CreateRepository<RepositoryForSpecialDataTypes>() )
                {
                    lager = new List<Zentrallager>(repository.SelectManyWhere<Zentrallager>(item => item.Teile.Contains(teil)));
                }
                AddCoordinates< Zentrallager>(lager,"koordinatenz", "Standort");
                return lager;
            }
            catch ( DatabaseException )
            {
                throw;
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Error in selecting all autoteile ") );
            }
        }

        /// <summary>
        /// Gets a json string containing the coordinates of the given lager and werkstatt
        /// </summary>
        /// <param name="lager">the list of lager</param>
        /// <param name="werkstatt">the werkstatt</param>
        /// <returns>string for two javascript variable containing the array
        ///          of lager coordinates and the coordinates of the werkstatt
        /// </returns>
        public static string GetJsonCoordinates( List<Zentrallager> lager, string werkstatt )
        {
            string ret = string.Empty;
            object[] arr = lager.Where(item => item.Coordinates != null).Select(item => new { name = item.Standort , coordinates = new { lat = item.Coordinates.X , lng = item.Coordinates.Y } }).ToArray();

            string s = string.Format("var lagerCoords = JSON.parse('{0}');" , JsonConvert.SerializeObject(arr , Formatting.None));

            Werkstatt w = GetWerkstatt(werkstatt);
            object toSer = new { name = w.Standort , coordinates = new { lat = w.Coordinates.X , lng = w.Coordinates.Y } };

            s += string.Format(" var werkstatt = JSON.parse('{0}');", JsonConvert.SerializeObject(toSer , Formatting.None));
            return s;
        }


        /// <summary>
        /// Gets the werkstatt
        /// </summary>
        /// <param name="werkstatt">the werkstatt</param>
        /// <returns>werkstatt</returns>
        private static Werkstatt GetWerkstatt(string werkstatt )
        {
            try
            {
                List<Werkstatt> lager = null;
                using ( repository = RepositoryFactory.Instance.CreateRepository<RepositoryForSpecialDataTypes>() )
                {
                    lager = new List<Werkstatt>(repository.SelectMany<Werkstatt>().AsEnumerable());
                }
                Werkstatt w = lager.Find(item => item.Standort.Equals(werkstatt));
                AddCoordinates<Werkstatt>(new List<Werkstatt>() { w } , "koordinaten" , "Standort");
                return w;

            }
            catch ( DatabaseException )
            {
                throw;
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Error in selecting all autoteile ") );
            }
        }

        /// <summary>
        /// Adds coordinates to a list of sdo objects
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lager"></param>
        /// <param name="sdoCol"></param>
        /// <param name="idCol"></param>
        private static void AddCoordinates<T>( List<T> lager , string sdoCol, string idCol) where T : SdoObject, IEntity
        {
            try
            {

                using ( repository = RepositoryFactory.Instance.CreateRepository<RepositoryForSpecialDataTypes>() )
                {
                    HashSet<Database.Common.Column> cols = new HashSet<Database.Common.Column>();
                    cols.Add(new Column() { Alias = "lon" , Name = "t.X" , Type = NHibernate.NHibernateUtil.String });
                    cols.Add(new Column() { Alias = "lat" , Name = "t.Y" , Type = NHibernate.NHibernateUtil.String });
                    foreach ( var item in lager )
                    {
                        //object[] coords =
                        IList lst = ( repository as RepositoryForSpecialDataTypes ).GetQuery(Database.Connection.Database.GetTableName<T>() + ", table(sdo_util.getvertices(" +sdoCol + ")) t" ,
                    string.Format("not " + sdoCol + " is null and " + Database.Connection.Database.GetColumnName<T>(idCol) + " = '{0}'" , item.Id) , cols); 
                        if ( lst.Count > 0 )
                        {
                            object[] coords = lst?[0] as object[] ?? default(object[]);
                            item.Coordinates = new Point(double.Parse(coords?[0]?.ToString()) , double.Parse(coords?[1]?.ToString()));
                        }
                    }
                }
            }
            catch ( DatabaseException )
            {
                throw;
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Error in selecting all autoteile ") );
            }
        }
        /// <summary>
        /// Gets all zentrallager
        /// </summary>
        /// <returns>list of zentrallager</returns>
        public static List<Zentrallager> GetZentrallager( )
        {
            try
            {
                List<Zentrallager> lager = null;
                using ( repository = RepositoryFactory.Instance.CreateRepository<RepositoryForSpecialDataTypes>() )
                {
                    lager = new List<Zentrallager>(repository.SelectMany<Zentrallager>().AsEnumerable());
                }
                AddCoordinates<Zentrallager>(lager, "koordinatenz", "Standort");
                return lager;
                
            }
            catch ( DatabaseException )
            {
                throw;
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Error in selecting all autoteile ") );
            }
        }
    }
}
