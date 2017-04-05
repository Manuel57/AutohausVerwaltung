using Database.Common;
using Database.Common.Impl;
using LagerverwaltungBL.Model;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Verwaltung.Exception;

namespace LagerverwaltungBL.Controller
{
    public class TeileManager
    {
        // private static IRepository repository = null;


        /// <summary>
        /// Returns all <see cref="Autoteile"/>
        /// </summary>
        /// <returns>all <see cref="Autoteile"/></returns>
        public static IEnumerable<Autoteile> GetAutoteile( )
        {
            try
            {
                using (IRepository repository = RepositoryFactory.Instance.CreateRepository<Repository>() )
                {
                    return new List<Autoteile>(repository.SelectMany<Autoteile>().AsEnumerable());
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
        /// Gets all <see cref="LagerverwaltungBL.Model.Autoteile"/>  of a <see cref="Werkstatt"/>
        /// </summary>
        /// <param name="standort">The Standort of the <see cref="Werkstatt"/></param>
        /// <returns></returns>
        public static IEnumerable<Autoteile> GetAutoteileWerkstatt( string standort )
        {
            try
            {
                using (IRepository repository = RepositoryFactory.Instance.CreateRepository<Repository>() )
                {
                    List<Werkstattlager> wl = new List<Werkstattlager>(repository.SelectMany<Werkstattlager>().AsEnumerable());
                    return wl.Where<Werkstattlager>(item => item.Werkstatt.Standort.Equals(standort))
                             .Select<Werkstattlager , Autoteile>(item => item.Teil);
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
        /// Gets alle <see cref="Autoteile"/> from the given <see cref="Werkstatt"/>
        /// where the amount is less than the given minBestand.
        /// Throws an exception if an error occurs
        /// </summary>
        /// <param name="standort">the name of the <see cref="Werkstatt"/></param>
        /// <param name="minBestand">the critical amout of <see cref="Autoteile"/></param>
        /// <returns>a List of <see cref="Autoteile"/></returns>
        public static IEnumerable<Autoteile> GetKritischeTeile( string standort, int minBestand)
        {
            try
            {
                using (IRepository repository = RepositoryFactory.Instance.CreateRepository<Repository>() )
                {
                    List<Werkstattlager> wl = new List<Werkstattlager>(
                        repository.SelectMany<Werkstattlager>().AsEnumerable());
                 
                    return wl?.Where<Werkstattlager>(item =>
                                        item.Werkstatt.Standort.Equals(standort) &&
                                        item.Bestand < minBestand)
                              .Select<Werkstattlager, Autoteile>(item => item.Teil);
                }
            }
            catch ( DatabaseException )
            {
                throw;
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Error in selecting critical autoteile ") );
            }

        }

        /// <summary>
        /// gets the amount of the <see cref="Autoteile"/> stored in the given 
        /// <see cref="Werkstatt"/>.
        /// </summary>
        /// <param name="standort">the name of the <see cref="Werkstatt"/></param>
        /// <param name="teil">the name of the <see cref="Autoteile"/></param>
        /// <returns>either default(int) or the amount of the <see cref="Autoteile"/> in the <see cref="Werkstatt"/></returns>
        public static int? GetBestand(string standort, string teil)
        {
            try
            {
                using (IRepository repository = RepositoryFactory.Instance.CreateRepository<Repository>())
                {
                    List<Werkstattlager> wl = new List<Werkstattlager>(
                        repository.SelectMany<Werkstattlager>().AsEnumerable());
                   
                   return wl?.FirstOrDefault<Werkstattlager>(item =>
                    item.Werkstatt.Standort.Equals(standort) &&
                    item.Teil.Bezeichnung.Equals(teil))?.Bestand ?? default(int);

                    
                }
            }
            catch (DatabaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw (new DatabaseException(ex, "Error in selecting all autoteile "));
            }

        }

        /// <summary>
        /// Creates a <see cref="Autoteile"/> and returns a copy of it
        /// </summary>
        /// <param name="bezeichnung">The bezeichnung</param>
        /// <param name="preis">The price</param>
        /// <returns>Copy of created <see cref="Autoteile"/></returns>
        public static Autoteile CreateAutoteil( string bezeichnung , double preis )
        {
            {
                try
                {
                    using (IRepository repository = RepositoryFactory.Instance.CreateRepository<Repository>() )
                    {
                        Autoteile autoteil = new Autoteile() { Bezeichnung = bezeichnung , Preis = preis };

                        repository.SaveOrUpdate(autoteil);

                        return autoteil.Clone() as Autoteile;
                    }

                }
                catch ( DatabaseException )
                {
                    throw;
                }
                catch ( Exception ex )
                {
                    throw ( new DatabaseException(ex , "Error in creating autoteil!") );
                }

            }

        }

        /// <summary>
        /// Orders a <see cref="Autoteile"/> to the given <see cref="Werkstatt"/>
        /// updates the inventory
        /// creates a new <see cref="Werkstattlager"/> if it has not already been existing
        /// </summary>
        /// <param name="bezeichnung">the <see cref="Autoteile.Bezeichnung"/> of the <see cref="Autoteile"/></param>
        /// <param name="werkstatt">the unique <see cref="Werkstatt.Standort"/> of the <see cref="Werkstatt"/></param>
        /// <param name="zentrallager">the unique <see cref="Zentrallager.Standort"/> of the <see cref="Zentrallager"/></param>
        /// <param name="quantity">the quantity</param>
        /// <returns>true if everything has been ok</returns>
        public static bool Order(string bezeichnung, string werkstatt, string zentrallager, int quantity)
        {
            Werkstattlager lager = null;
            try
            {
                using (IRepository repository = RepositoryFactory.Instance.CreateRepository<Repository>() )
                {
                    long count = repository.CountWhere<Werkstattlager>(DetachedCriteria.For<Werkstattlager>()
                                                        .Add(Restrictions.Where<Werkstattlager>(item => item.Werkstatt.Standort == (werkstatt)))
                                                        .Add(Restrictions.Where<Werkstattlager>(item => item.Teil.Bezeichnung == (bezeichnung))));
                    if (count > 0)
                    {

                        lager = repository.SelectSingleWhere<Werkstattlager>(item => item.Werkstatt.Standort.Equals(werkstatt) && item.Teil.Bezeichnung.Equals(bezeichnung));
                        lager.Bestand += quantity;
                    }
                    else
                    {
                        Werkstatt w = repository.SelectSingleWhere<Werkstatt>(item => item.Standort.Equals(werkstatt));
                        Autoteile a = repository.SelectSingleWhere<Autoteile>(item => item.Bezeichnung.Equals(bezeichnung));

                        lager = new Werkstattlager() { Werkstatt = w, Teil = a, Bestand = quantity };
                    }

                    repository.SaveOrUpdate<Werkstattlager>(lager);
                }
                return true;
            }
            catch ( DatabaseException )
            {
                throw;
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Error at ordering") );
            }

        }

        /// <summary>
        /// selects the names of all <see cref="Werkstatt"/> from the database
        /// </summary>
        /// <returns>a list of all names</returns>
        public static IEnumerable<string> AllWerkstattNames()
        {

            try
            {
                using (IRepository repository = RepositoryFactory.Instance.CreateRepository<Repository>())
                {
                    List<string> ret = new List<string>();
                    ret = repository.SelectMany<Werkstatt>().AsEnumerable()
                                     .Select<Werkstatt, string>(item => item.Standort).ToList();



                    return ret;
                }
            }
            catch (DatabaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw (new DatabaseException(ex, ""));
            }

        }
   }
}
