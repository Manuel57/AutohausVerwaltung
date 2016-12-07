﻿// <copyright file="BenutzerverwaltungBL.Controller.RechnungManager.cs">
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

        /// <summary>
        /// inserts the bill as pdf in the db
        /// the titel is genrated out of the details of the bill
        /// throws an exception if an error occurs
        /// </summary>
        /// <param name="rechnungn"></param>
        /// <returns>true or throws an exception</returns>
        public static bool InsertRechnungAsDoc(Rechnung rechnungn)
        {
            try
            {
                bool ret = true;
                using (repository = RepositoryFactory.Instance.CreateRepository<Repository>())
                {
                    ISQLQuery query = repository.GetQuery("insert into " + TABLERECHNUNGDOCS + "(Title,Text) values (?,?)");
                    query.SetString(0, GenerateTitel(rechnungn));
                    query.SetParameter(1, GeneratePDF(rechnungn),NHibernateUtil.BinaryBlob);                  
                   
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
                    query.AddScalar("text",NHibernateUtil.BinaryBlob);
                    var all = query.List();
                   
                    foreach (var s in all)
                    {                        
                        ret.Add(s as byte[]);
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

        /// <summary>
        /// selects a certain bill of the customer given 
        /// by checking the date given
        /// </summary>
        /// <param name="customerID">the customer whos bill to select</param>
        /// <param name="rechnungsDatum">the date of which the bill is</param>
        /// <returns> a byte [] or null or throws an exception </returns>
        public static byte[] GetCertainRechnungForKunde(int customerID, DateTime rechnungsDatum)
        {
            try
            {
                byte[] ret = null;

                using (repository = RepositoryFactory.Instance.CreateRepository<Repository>())
                {
                    ISQLQuery query = repository.GetQuery("select text from " + TABLERECHNUNGDOCS + " r where r.title like ?");
                    query.SetString(0, customerID + "%" + rechnungsDatum.ToShortDateString());
                    query.AddScalar("text", NHibernateUtil.BinaryBlob);
                    ret = query.UniqueResult() as byte[];
                   
                }

                return ret;
            }
            catch (DatabaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw (new DatabaseException(ex, "Keine Rechnung vorhanden!"));
            }

        }

        /// <summary>
        /// calls the PdfGenerator to Generate a pdf out of the 
        /// given bill.
        /// returns the pdf as byte[]
        /// </summary>
        /// <param name="rechnungn"></param>
        /// <returns>a pdf as byte[]</returns>
        private static byte[] GeneratePDF(Rechnung rechnungn)
        {
            return PdfGenerator.GeneratePDF(rechnungn);
        }
      
        /// <summary>
        /// generates the titel for storing in the database
        /// out of the given bill
        /// </summary>
        /// <param name="rechnungn"></param>
        /// <returns>a generated titel</returns>
        private static string GenerateTitel(Rechnung rechnungn)
        {            
            return rechnungn.Kunde.CustomerId+rechnungn.Kunde.LastName + "_" + rechnungn.Rechnungsdatum.ToShortDateString();
        }
    }
}
