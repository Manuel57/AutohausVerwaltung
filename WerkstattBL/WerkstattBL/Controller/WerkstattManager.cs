using Database.Common;
using Database.Common.Impl;
using NHibernate.Criterion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verwaltung.Exception;
using WerkstattBL.Model;

namespace WerkstattBL.Controller
{
    public static class WerkstattManager
    {
        public static IEnumerable<KundenRechnungsHilfe> GetMessagesForToday( DateTime now )
        {

            try
            {
                using ( IRepository repository = RepositoryFactory.Instance.CreateRepository<Repository>() )
                {
                    List<KundenRechnungsHilfe> ret = repository.SelectManyWhere<KundenRechnungsHilfe>(DetachedCriteria.For<KundenRechnungsHilfe>()
                                                                .Add(Restrictions.Where<KundenRechnungsHilfe>(item => item.Datum.ToShortDateString() == now.ToShortDateString())
                        )).ToList();
                    return ret;
                }
            }
            catch ( DatabaseException )
            {
                throw;
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Error in getting Messages for today!") );
            }

        }
        public static bool CreateReparatur( int repartID , int rechnungsnummer , DateTime date , string standort )
        {

            try
            {
                using ( IRepository repository = RepositoryFactory.Instance.CreateRepository<Repository>() )
                {
                    bool ret = false;
                    Reparaturart rArt = repository.SelectSingleWhere<Reparaturart>(item => item.ReparaturArtId == repartID);
                    Reparatur rep = new Reparatur()
                    {
                        ReparaturId = repository.Max<Reparatur , int>("ReparaturId") + 1 ,
                        Rechnungsnummer = rechnungsnummer ,
                        RepArt = rArt ,
                        Standort = standort ,
                        ReparaturDatum = date
                    };

                    repository.SaveOrUpdate<Reparatur>(rep);
                    //für reparatur benötigte Teile aus Bestand nehmen
                    DecreaseMenge(repartID , standort);

                    ret = true;

                    return ret;
                }
            }
            catch ( DatabaseException )
            {
                throw;
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Error in creating a Reparatur!") );
            }

        }
        public static void DeleteFromHelp( int rechnungsnummer , int kundenID )
        {

            try
            {
                using ( IRepository repository = RepositoryFactory.Instance.CreateRepository<Repository>() )
                {
                    IDictionary dic = new Dictionary<string , int>();
                    dic.Add("KundenID" , kundenID);
                    dic.Add("Rechnungsnummer" , rechnungsnummer);

                    repository.SelectManyWhere<KundenRechnungsHilfe>(DetachedCriteria.For<KundenRechnungsHilfe>()
                                                                    .Add(Restrictions.AllEq(dic)))
                                                                    .ToList()
                                                                    .ForEach(item => repository.Delete(item));
                }
            }
            catch ( DatabaseException )
            {
                throw;
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Error in delting messages!") );
            }

        }

        public static IEnumerable<string> GetAlleStandort()
        {

            try
            {
                using (IRepository repository = RepositoryFactory.Instance.CreateRepository<Repository>())
                {
                  
                    IList<string> ret = (repository.GetQuery("select standort from werkstatt")).List<string>();
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

        public static IEnumerable<Reparaturart> GetAlleFürStandort(string standort)
        {
            using (IRepository repository = RepositoryFactory.Instance.CreateRepository<Repository>())
            {
                IList<int> repArtId = (repository.GetQuery("select reperaturartid from reparturangebot where standort = ?").SetString(0,standort)
                                    ).List<int>();
                IEnumerable<Reparaturart> ret = repository.SelectManyWhere<Reparaturart>(item => repArtId.Contains(item.ReparaturArtId));
                return ret;
            }
        }
        private static void DecreaseMenge( int repartID , string standort )
        {

            try
            {
                using ( IRepository repository = RepositoryFactory.Instance.CreateRepository<Repository>() )
                {

                    List<Reparaturteile> repTeile = repository.SelectMany<Reparaturteile>()
                                                                .Where(item => item.RepArt.ReparaturArtId == repartID)
                                                                .ToList();

                    //könnt a anders gmacht werden mit Restriction.In oda so
                    List<Werkstattlager> wl = repository.SelectManyWhere<Werkstattlager>(DetachedCriteria.For<Werkstattlager>()
                                                            .Add(Restrictions.Where<Werkstattlager>(item => item.Werkstatt == standort))
                                                            .Add(Restrictions.Where<Autoteile>(item => repTeile.Select<Reparaturteile , Autoteile>(teil => teil.Teil).Contains(item)))
                                                            ).ToList();

                    wl.ForEach(item => item.Bestand -= repTeile.Find(teil => teil.Teil == item.Teil).Menge);


                    //Please try the version below

                    //List<Reparaturteile> z = null;
                    //( from we in repository.SelectMany<Werkstattlager>()
                    //  let l = ( from s in repository.SelectMany<Reparaturteile>()
                    //            where s.RepArt.ReparaturArtId.Equals(repartID)
                    //            select s )
                    //  let d = new { z = l }
                    //  where we.Werkstatt
                    //  .Equals(standort) && (
                    //      from x in l select x.Teil ).Contains(we.Teil)

                    //  select we ).ToList().ForEach(item => item.Bestand -=
                    //  ( ( List<Reparaturteile> ) ( from t in z
                    //                               where t.Teil.Equals(item.Teil)
                    //                               select t ) ).First().Menge);

                    repository.SaveOrUpdateMore(wl);
                }
            }
            catch ( DatabaseException )
            {
                throw;
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Error in decreasing amount of Teile") );
            }

        }

    }
}
