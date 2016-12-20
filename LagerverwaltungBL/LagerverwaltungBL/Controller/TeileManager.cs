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


        public static IEnumerable<Werkstattlager> GetAutoteileWerkstatt(string standort)
        {
            try
            {
                using ( repository = RepositoryFactory.Instance.CreateRepository<Repository>() )
                {
                    List<Werkstattlager> wl = new List<Werkstattlager>(repository.SelectMany<Werkstattlager>().AsEnumerable());
                    return wl.Where<Werkstattlager>(item=>item.Lager.Standort.Equals(standort));
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
