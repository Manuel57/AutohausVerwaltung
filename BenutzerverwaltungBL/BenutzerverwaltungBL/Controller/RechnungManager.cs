// <copyright file="BenutzerverwaltungBL.Controller.RechnungManager.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-14</date>
// </copyright>

using BenutzerverwaltungBL.Model.DataObjects;
using Database;
using Database.Common;
using Database.Common.Impl;
using NHibernate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenutzerverwaltungBL.Controller
{
    public static class RechnungManager
    {
        //aus der rechnung a pfd gnerieren und ins oracle docs tabelle speichern
        // und get von rechnungen ( list von rechnungsnummern oder text durchsuchen)

        #region private fields
        private static string SEARCHFFORRECHNUNGEN = "SEARCHFORRECHNUNG";
        private static string TABLERECHNUNGDOCS = "RECHNUNGDOCS";
        private static string TABLERECHNUNGHELP = "RECHNUNGDOCSHELPTABLE";
        private static IRepository repository = null;
        #endregion

        //sollte dann nur mehr mit der rechnung gehen und das byte[] selber erzeugen
        public static bool InsertRechnungAsDoc(Rechnung rechnungn, byte [] doc)
        {
            try
            {
                bool ret = true;
                using (repository = RepositoryFactory.Instance.CreateRepository<Repository>())
                {
                    int id = rechnungn.Rechnungsdatum.Millisecond + Convert.ToInt32(rechnungn.Gesamtpreis);
                    ISQLQuery query = repository.GetQuery("insert into " + TABLERECHNUNGDOCS + "(ID,Title,Text) values (?,?,?)");
                    query.SetInt32(0, id);
                    query.SetString(1, GenerateTitel(rechnungn));
                    // query.SetParameter(2, GenerateDoc(rechnungn),NHibernateUtil.BinaryBlob);                  
                    query.SetParameter(2, doc, NHibernateUtil.BinaryBlob);
                    query.ExecuteUpdate();
                }
                return ret;
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
        /// docs for the given customer id
        /// </summary>
        /// <param name="customerID">the id of the customer</param>
        public static List<byte[]> GetAllRechnungenForKunde(int customerID)
        {
            try
            {
                List<byte[]> ret = new List<byte[]>();           
                using (repository = RepositoryFactory.Instance.CreateRepository<Repository>())
                {
                    ISQLQuery query = repository.GetQuery("select text from " + TABLERECHNUNGDOCS +" r where r.title like ?");
                    query.SetString(0, "%" + customerID+"%");
                    query.AddScalar("title", NHibernateUtil.String);
                    query.AddScalar("text",NHibernateUtil.BinaryBlob);
                    var all = query.List();
                   
                    foreach (var s in all)
                    {                        
                        ret.Add(s as byte []);
                    }
                }
                return ret;
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

        private static byte[] GenerateDoc(Rechnung rechnungn)
        {
            // hier aus objekt die pdf erzeugen 
            throw new NotImplementedException();
        }
      
        /// <summary>
        /// generates the titel for storing in the database
        /// out of the given bill
        /// </summary>
        /// <param name="rechnungn"></param>
        /// <returns>a generated titel</returns>
        private static string GenerateTitel(Rechnung rechnungn)
        {
            return rechnungn.Kunde.CustomerId+rechnungn.Kunde.FullName + "_" + rechnungn.Rechnungsdatum.ToShortDateString();
        }
    }
}
