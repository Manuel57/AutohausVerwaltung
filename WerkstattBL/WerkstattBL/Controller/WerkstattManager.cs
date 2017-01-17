using Database.Common;
using Database.Common.Impl;
using NHibernate.Criterion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verwaltung.Exception;
using WerkstattBL.Model;

namespace WerkstattBL.Controller
{
   public static class WerkstattManager
    {       
        public static IEnumerable<KundenRechnungsHilfe> GetMessagesForToday(DateTime now)
        {

            try
            {
                using (IRepository repository = RepositoryFactory.Instance.CreateRepository<Repository>())
                {
                    List<KundenRechnungsHilfe> ret = repository.SelectManyWhere<KundenRechnungsHilfe>(DetachedCriteria.For<KundenRechnungsHilfe>()
                                                                .Add(Restrictions.Where<KundenRechnungsHilfe>(item => item.Datum.ToShortDateString() == now.ToShortDateString())
                        )).ToList();
                    return ret;
                }
            }
            catch (DatabaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw (new DatabaseException(ex, "Error in getting Messages for today!"));
            }

        }
        public static bool CreateReparatur(int repartID, int rechnungsnummer,DateTime date,string standort)
        {

            try
            {
                using (IRepository repository = RepositoryFactory.Instance.CreateRepository<Repository>())
                {
                    bool ret = false;
                    Reparaturart rArt = repository.SelectSingleWhere<Reparaturart>(item => item.ReparaturArtId == repartID);
                    Reparatur rep = new Reparatur()
                    {
                        ReparaturId = repository.Max<Reparatur, int>("ReparaturId") + 1,
                        Rechnungsnummer = rechnungsnummer,
                        RepArt = rArt,
                        Standort = standort,
                        ReparaturDatum = date
                    };

                    repository.SaveOrUpdate<Reparatur>(rep);
                    //für reparatur benötigte Teile aus Bestand nehmen
                    DecreaseMenge(repartID, standort);

                    ret = true;

                    return ret;
                }
            }
            catch (DatabaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw (new DatabaseException(ex, "Error in creating a Reparatur!"));
            }

        }
        public static void DeleteFromHelp(int rechnungsnummer,int kundenID)
        {

            try
            {
                using (IRepository repository = RepositoryFactory.Instance.CreateRepository<Repository>())
                {
                    IDictionary dic = new Dictionary<string, int>();
                    dic.Add("KundenID", kundenID);
                    dic.Add("Rechnungsnummer", rechnungsnummer);

                    repository.SelectManyWhere<KundenRechnungsHilfe>(DetachedCriteria.For<KundenRechnungsHilfe>()
                                                                    .Add(Restrictions.AllEq(dic)))
                                                                    .ToList()
                                                                    .ForEach(item => repository.Delete(item));
                }
            }
            catch (DatabaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw (new DatabaseException(ex, "Error in delting messages!"));
            }

        }
        private static void DecreaseMenge(int repartID, string standort)
        {

            try
            {
                using (IRepository repository = RepositoryFactory.Instance.CreateRepository<Repository>())
                {

                    List<Reparaturteile> repTeile = repository.SelectMany<Reparaturteile>()
                                                                .Where(item => item.RepArt.ReparaturArtId == repartID)
                                                                .ToList();
                    List<Autoteile> teile = repTeile.Select<Reparaturteile, Autoteile>(item => item.Teil).ToList();
                    //könnt a anders gmacht werden mit Restriction.In oda so
                    List<Werkstattlager> wl = repository.SelectManyWhere<Werkstattlager>(DetachedCriteria.For<Werkstattlager>()
                                                            .Add(Restrictions.Where<Werkstattlager>(item => item.Werkstatt == standort))
                                                            .Add(Restrictions.In("Teil",new Collection<Autoteile>(teile)))
                                                            ).ToList();

                    wl.ForEach(item => item.Bestand -= repTeile.Find(teil => teil.Teil == item.Teil).Menge);

                    repository.SaveOrUpdateMore(wl);
                }
            }
            catch (DatabaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw (new DatabaseException(ex, "Error in decreasing amount of Teile"));
            }

        } 

    }
}
