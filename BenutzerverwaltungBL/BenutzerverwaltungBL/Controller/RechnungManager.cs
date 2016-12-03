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

        public static bool InsertRechnungAsDoc(Rechnung rechnungn)
        {
            try
            {
                bool ret = true;
                using (repository = RepositoryFactory.Instance.CreateRepository<Repository>())
                {
                    int id = rechnungn.Rechnungsdatum.Millisecond + Convert.ToInt32(rechnungn.Gesamtpreis);
                    ISQLQuery query = repository.GetQuery("insert into " + TABLERECHNUNGDOCS + "(ID,Titel,Text) values (:id,:titel,:doc)");
                    query.SetInt32(":id", id);
                    query.SetString(":titel", GenerateTitel(rechnungn));
                   // query.SetBinary(":doc", GenerateDoc(rechnungn));
                    query.SetParameter(":doc", GenerateDocString(rechnungn), NHibernateUtil.StringClob);
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

        private static byte[] GenerateDoc(Rechnung rechnungn)
        {
            // hier aus objekt die pdf erzeugen 
            throw new NotImplementedException();
        }
        private static string GenerateDocString(Rechnung rechnungn)
        {
            // hier aus objekt die pdf erzeugen 
            throw new NotImplementedException();
        }


        private static string GenerateTitel(Rechnung rechnungn)
        {
            return rechnungn.Kunde.FullName + "_" + rechnungn.Rechnungsdatum.ToShortDateString();
        }
    }
}
