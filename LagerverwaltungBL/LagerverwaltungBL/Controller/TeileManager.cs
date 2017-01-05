using Database.Common;
using Database.Common.Impl;
using LagerverwaltungBL.Model;
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
        private static IRepository repository = null;


        /// <summary>
        /// Returns all Teile
        /// </summary>
        /// <returns>all Teile</returns>
        public static IEnumerable<Autoteile> GetAutoteile( )
        {
            try
            {
                using ( repository = RepositoryFactory.Instance.CreateRepository<Repository>() )
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
        /// Gets all Teile of a Werkstatt
        /// </summary>
        /// <param name="standort">The Standort of the Werkstatt</param>
        /// <returns></returns>
        public static IEnumerable<Autoteile> GetAutoteileWerkstatt( string standort )
        {
            try
            {
                using ( repository = RepositoryFactory.Instance.CreateRepository<Repository>() )
                {
                    List<Werkstattlager> wl = new List<Werkstattlager>(repository.SelectMany<Werkstattlager>().AsEnumerable());
                    return wl.Where<Werkstattlager>(item => item.Werkstatt.Standort.Equals(standort)).Select<Werkstattlager , Autoteile>(item => item.Teil);
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

       
        public static IEnumerable<Autoteile> GetKritischeTeile( string standort, int minBestand)
        {
            try
            {
                using ( repository = RepositoryFactory.Instance.CreateRepository<Repository>() )
                {
                    List<Werkstattlager> wl = new List<Werkstattlager>(
                        repository.SelectMany<Werkstattlager>().AsEnumerable());
                    List<Autoteile> ret = new List<Autoteile>();

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
                throw ( new DatabaseException(ex , "Error in selecting all autoteile ") );
            }

        }

        public static int? GetBestand(string standort, string teil)
        {
            try
            {
                using (repository = RepositoryFactory.Instance.CreateRepository<Repository>())
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
        /// Creates a teil and returns a copy of it
        /// </summary>
        /// <param name="bezeichnung">The bezeichnung</param>
        /// <param name="preis">The price</param>
        /// <returns>Copy of created teil</returns>
        public static Autoteile CreateAutoteil( string bezeichnung , double preis )
        {
            {
                try
                {
                    using ( repository = RepositoryFactory.Instance.CreateRepository<Repository>() )
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
    }
}
