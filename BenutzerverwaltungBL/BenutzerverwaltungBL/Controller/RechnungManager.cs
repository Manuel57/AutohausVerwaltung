// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-14</date>

using BenutzerverwaltungBL.Model.DataObjects;
using Database;
using Database.Common;
using Database.Common.Impl;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Verwaltung.Exception;

namespace BenutzerverwaltungBL.Controller
{
    public static class RechnungManager
    {

        #region private fields     
        private static string TABLERECHNUNGDOCS = "RECHNUNGDOCS";
        //private static IRepository repository = null;
        #endregion

        /// <summary>
        /// inserts the  <see cref="BenutzerverwaltungBL.Model.DataObjects.Rechnung"/> as pdf in the db
        /// the titel is genrated out of the details of the  <see cref="BenutzerverwaltungBL.Model.DataObjects.Rechnung"/>
        /// throws an exception if an error occurs
        /// </summary>
        /// <param name="rechnungsId">the identifier of the  <see cref="BenutzerverwaltungBL.Model.DataObjects.Rechnung"/></param>
        /// <returns>true or throws an exception</returns>
        public static bool InsertRechnungAsDoc( int rechnungsId )
        {
            try
            {
                bool ret = true;
                Rechnung rechnungn;
                using (IRepository repository = RepositoryFactory.Instance.CreateRepository<Repository>() )
                {
                    rechnungn = repository.SelectSingle<Rechnung>(DetachedCriteria.For<Rechnung>()
                                                                  .Add(Restrictions.IdEq(rechnungsId)));

                    ISQLQuery query = repository.GetQuery("insert into " + TABLERECHNUNGDOCS + "(Title,Text) values (?,?)");
                    query.SetString(0 , GenerateTitel(rechnungn));
                    query.SetParameter(1 , GeneratePDF(rechnungn) , NHibernateUtil.BinaryBlob);

                    query.ExecuteUpdate();

                    rechnungn.IsAlreadyPdf = true;
                    repository.SaveOrUpdate<Rechnung>(rechnungn);

                }
                
                return ret;
            }
            catch ( DatabaseException )
            {
                throw;
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Fehler beim ausstellen der Rechnung", "Überprüfen Sie ob die Rechnung bereits aussgestellt wurde", "Wenn Sie keine Lösung finden, melden Sie sich bei unserem Service Team.", "Danke") );
            }

        }

        /// <summary>
        /// inserts all  <see cref="BenutzerverwaltungBL.Model.DataObjects.Rechnung"/>, which have not already been safed
        /// as pdf, in the DB as PDF
        /// </summary>
        /// <param name="customerID">the customer identifier</param>
        public static void InsertAllRechnungAsDoc(Customer c,int customerID)
        {
            try
            {               
                   // Customer c = CustomerManager.GetSingleCustomerById(customerID);
                    c.Rechnungen.ToList()
                                .Where(item => item.IsAlreadyPdf == false).ToList()
                                .ForEach(item => InsertRechnungAsDoc(item.Rechnungsnummer));               
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

        /// <summary>
        /// returns a list of byte[] containing all
        /// pfds for the given customer id
        /// </summary>
        /// <param name="customerID">the id of the customer</param>
        public static List<byte[]> GetAllRechnungenForKunde( int customerID )
        {
            try
            {
                List<byte[]> ret = new List<byte[]>();
                using ( IRepository repository = RepositoryFactory.Instance.CreateRepository<Repository>() )
                {
                    ISQLQuery query = repository.GetQuery("select text from " + TABLERECHNUNGDOCS + " r where r.title like ?");
                    query.SetString(0 , "%" + customerID + "%");
                    query.AddScalar("text" , NHibernateUtil.BinaryBlob);
                    var all = query.List();

                    foreach ( var s in all )
                    {
                        ret.Add(s as byte[]);
                    }
                }
                return ret;
            }
            catch ( DatabaseException )
            {
                throw;
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "") );
            }

        }

        /// <summary>
        /// selects a certain  <see cref="BenutzerverwaltungBL.Model.DataObjects.Rechnung"/> of the customer given 
        /// by checking the date given
        /// </summary>
        /// <param name="customerID">the customer whos  <see cref="BenutzerverwaltungBL.Model.DataObjects.Rechnung"/> to select</param>
        /// <param name="rechnungsID">the id of the  <see cref="BenutzerverwaltungBL.Model.DataObjects.Rechnung"/></param>
        /// <returns> a byte [] or null or throws an exception </returns>
        public static byte[] GetCertainRechnungForKunde( int customerID , int rechnungsID )
        {
            try
            {
                byte[] ret = null;

                using ( IRepository repository = RepositoryFactory.Instance.CreateRepository<Repository>() )
                {
                    Rechnung r = repository.SelectSingle<Rechnung>(DetachedCriteria.For<Rechnung>()
                                                                  .Add(Restrictions.IdEq(rechnungsID)));

                    ISQLQuery query = repository.GetQuery("select text from " + TABLERECHNUNGDOCS + " r where r.title like ?");
                    query.SetString(0 , customerID + "%" + r.Rechnungsdatum.ToShortDateString());
                    query.AddScalar("text" , NHibernateUtil.BinaryBlob);
                    ret = query.UniqueResult() as byte[];

                }

                return ret;
            }
            catch ( DatabaseException )
            {
                throw;
            }
            catch ( Exception ex )
            {
                throw ( new DatabaseException(ex , "Keine Rechnung vorhanden!") );
            }

        }

        /// <summary>
        /// calls the PdfGenerator to Generate a pdf out of the 
        /// given  <see cref="BenutzerverwaltungBL.Model.DataObjects.Rechnung"/>.
        /// returns the pdf as byte[]
        /// </summary>
        /// <param name="rechnungn"></param>
        /// <returns>a pdf as byte[]</returns>
        private static byte[] GeneratePDF( Rechnung rechnungn )
        {
            return PdfGenerator.GeneratePDF(rechnungn);
        }

        /// <summary>
        /// generates the titel for storing in the database
        /// out of the given  <see cref="BenutzerverwaltungBL.Model.DataObjects.Rechnung"/>
        /// </summary>
        /// <param name="rechnungn"></param>
        /// <returns>a generated titel</returns>
        private static string GenerateTitel( Rechnung rechnungn )
        {
            return rechnungn.Kunde.CustomerId + rechnungn.Kunde.LastName + "_" + rechnungn.Rechnungsdatum.ToShortDateString();
        }
    }
}
