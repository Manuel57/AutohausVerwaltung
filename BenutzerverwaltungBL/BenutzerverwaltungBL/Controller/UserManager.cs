using BenutzerverwaltungBL.Model.DataObjects;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Database;
namespace BenutzerverwaltungBL.Controller
{
    public static class UserManager
    {
        #region private fields
        private static string DEFAULTWERKSTATTKONZERN = "THE MECHANICS";
        #endregion
        #region methods
        public static Customer CreateCustomer(string werkstattKonzern,string fullName, DateTime birthDate,
                                        string adresse, string username, string password)
        {

            throw new NotImplementedException();
        }

        public static Customer GetSingleCustomerById(int id)
        {
            throw new NotImplementedException();
        }

        public static Customer GetSingleCustomerByExpression(Expression<Func<Customer, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Customer> GetAllCutomer()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
