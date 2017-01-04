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


        public static string GetJsonCoordinates( Autoteile teil ,string werkstat)
        {
            string ret = string.Empty;
            ret = GetJsonCoordinates(GetZentrallagerByTeil(teil), werkstat);
            return ret;
        }

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

        public static string GetJsonCoordinates( List<Zentrallager> lager, string werkstatt )
        {
            string ret = string.Empty;
            object[] arr = lager.Where(item => item.Coordinates != null).Select(item => new { name = item.Standort , coordinates = new { lat = item.Coordinates.X , lng = item.Coordinates.Y } }).ToArray();




            string s = string.Format("var lagerCoords = JSON.parse('{0}');" , JsonConvert.SerializeObject(arr , Formatting.None));

            Werkstatt w = GetWerkstatt(werkstatt);
            object toSer = new { name = w.Standort , coordinates = new { lat = w.Coordinates.X , lng = w.Coordinates.Y } };

            s += string.Format(" var werkstatt = JSON.parse('{0}');", JsonConvert.SerializeObject(toSer , Formatting.None));
            //s += " var werkstatt = { name: 'The mechanics', coordinates: { lat: 46.6188400, lng: 13.822790000000007 } }";


            //List<string> l = arr.Select(item => "getLongLat(JSON.parse('" + JsonConvert.SerializeObject(item) + "'))").ToList();
            //l.Insert(l.Count , "");
            //string s = string.Format("function initMap() <| {0} |>" , string.Join(";" , l.ToArray()));

            //s = s.Replace("<|" , "{").Replace("|>" , "}");


            return s;
        }

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
