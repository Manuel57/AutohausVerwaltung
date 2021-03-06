﻿// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2017-1-3</date>

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
        /// Creates a json string containing the coordinates of all <see cref="LagerverwaltungBL.Model.Zentrallager"/> having the given <see cref="LagerverwaltungBL.Model.Autoteile"/> instock
        /// </summary>
        /// <param name="teil">the <see cref="LagerverwaltungBL.Model.Autoteile"/></param>
        /// <param name="werkstatt">the <see cref="LagerverwaltungBL.Model.Werkstatt"/></param>
        /// <returns>json string</returns>
        public static string GetJsonCoordinates( Autoteile teil , string werkstatt )
        {
            string ret = string.Empty;
            ret = GetJsonCoordinates(GetZentrallagerByTeil(teil) , werkstatt);
            return ret;
        }
       
        /// <summary>
        /// Gets all <see cref="LagerverwaltungBL.Model.Zentrallager"/> having the given <see cref="LagerverwaltungBL.Model.Autoteile"/>
        /// </summary>
        /// <param name="teil">the <see cref="LagerverwaltungBL.Model.Autoteile"/></param>
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
                AddCoordinates<Zentrallager>(lager , "koordinatenz" , "Standort");
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
        /// Gets a json string containing the coordinates of the given list of <see cref="LagerverwaltungBL.Model.Zentrallager"/> and <see cref="LagerverwaltungBL.Model.Werkstatt"/>
        /// </summary>
        /// <param name="lager">the list of <see cref="Zentrallager"/></param>
        /// <param name="werkstatt">the <see cref="Werkstatt.Standort"/></param>
        /// <returns>string for two javascript variable containing the array
        ///          of lager coordinates and the coordinates of the werkstatt
        /// </returns>
        public static string GetJsonCoordinates( List<Zentrallager> lager , string werkstatt )
        {
            string ret = string.Empty;
            object[] arr = lager.Where(item => item.Coordinates != null).Select(item => new { name = item.Standort , coordinates = new { lat = item.Coordinates.X , lng = item.Coordinates.Y } }).ToArray();

            string s = string.Format("var lagerCoords = JSON.parse('{0}');" , JsonConvert.SerializeObject(arr , Formatting.None));

            Werkstatt w = GetWerkstatt(werkstatt);
            object toSer = new { name = w.Standort , coordinates = new { lat = w.Coordinates.X , lng = w.Coordinates.Y } };

            s += string.Format(" var werkstatt = JSON.parse('{0}');" , JsonConvert.SerializeObject(toSer , Formatting.None));
            return s;
        }


        /// <summary>
        /// Gets the <see cref="Werkstatt"/>
        /// </summary>
        /// <param name="werkstatt">the <see cref="Werkstatt"/></param>
        /// <returns>a <see cref="Werkstatt"/></returns>
        private static Werkstatt GetWerkstatt( string werkstatt )
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
        private static void AddCoordinates<T>( List<T> lager , string sdoCol , string idCol ) where T : SdoObject, IEntity
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
                        IList lst = ( repository as RepositoryForSpecialDataTypes ).GetQuery(Database.Connection.Database.GetTableName<T>() + ", table(sdo_util.getvertices(" + sdoCol + ")) t" ,
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
        /// Gets all <see cref="Zentrallager"/>
        /// </summary>
        /// <returns>list of <see cref="Zentrallager"/></returns>
        public static List<Zentrallager> GetZentrallager( )
        {
            try
            {
                List<Zentrallager> lager = null;
                using ( repository = RepositoryFactory.Instance.CreateRepository<RepositoryForSpecialDataTypes>() )
                {
                    lager = new List<Zentrallager>(repository.SelectMany<Zentrallager>().AsEnumerable());
                }
                AddCoordinates<Zentrallager>(lager , "koordinatenz" , "Standort");
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
