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
        public static IEnumerable<Kundenrechhilfe> GetMessagesForToday( DateTime now, string sta)
        {

            try
            {
                using ( IRepository repository = RepositoryFactory.Instance.CreateRepository<Repository>() )
                {
                    //
                    List<Kundenrechhilfe> ret = new List<Kundenrechhilfe>(repository.SelectManyWhere<Kundenrechhilfe>(DetachedCriteria.For<Kundenrechhilfe>()
                                                                                           .Add(Restrictions.Where<Kundenrechhilfe>(item => item.Standort == sta))
                                                                ));
                    return ret.Where(item => item.Datum.ToShortDateString() == now.ToShortDateString());
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

                    repository.SelectManyWhere<Kundenrechhilfe>(DetachedCriteria.For<Kundenrechhilfe>()
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
                    return new List<string>( ret);
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
                IList<decimal> repArtId = (repository.GetQuery("select Reparaturartid from REparaturangebot where standort = ?").SetString(0,standort)
                                    ).List<decimal>();
                IEnumerable<Reparaturart> ret = repository.SelectManyWhere<Reparaturart>(item => repArtId.Contains(item.ReparaturArtId));
                return new List<Reparaturart>(ret);
            }
        }
        private static void DecreaseMenge( int repartID , string standort )
        {

            try
            {
                using ( IRepository repository = RepositoryFactory.Instance.CreateRepository<Repository>() )
                {

                    List<Reparaturteile> repTeile = new List<Reparaturteile>( repository.SelectManyWhere<Reparaturteile>(
                                                                                DetachedCriteria.For<Reparaturteile>()
                                                                                 .Add(Restrictions.Where<Reparaturteile>(item => item.RepArt.ReparaturArtId == repartID)
                                                                            )));
                    List<Autoteile> autoteile = repTeile.Select(teil => teil.Teil).ToList();
                     //könnt a anders gmacht werden mit Restriction.In oda so
                    List<Werkstattlager> wl = new List<Werkstattlager>(repository.SelectManyWhere<Werkstattlager>(DetachedCriteria.For<Werkstattlager>()
                                                            .Add(Restrictions.Where<Werkstattlager>(item => item.Standort == standort)))
                                                             .ToList());
                    wl = wl.Where(item => autoteile.Contains(item.Teil)).ToList();
                    wl.ForEach(item => item.Bestand -= repTeile.Find(teil => teil.Teil == item.Teil).Menge);


                    //Just another way to do this with linq

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
