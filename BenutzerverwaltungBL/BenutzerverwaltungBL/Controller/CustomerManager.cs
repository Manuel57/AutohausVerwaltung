using BenutzerverwaltungBL.Model.DataObjects;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Database;
using Database.Common;
using Database.Common.Impl;
using BenutzerverwaltungBL.Model.BusinesObjects;
using BenutzerverwaltungBL.Common;

namespace BenutzerverwaltungBL.Controller
{
    public static class CutomerManager
    {
        #region private fields
        private static string DEFAULTWERKSTATTKONZERN = "THE MECHANICS";
        #endregion
        #region methods
        public static Customer CreateCustomer(string werkstattKonzern,string fullName, DateTime birthDate,
                                        string adresse)
        {

            try
            {
                using (IRepository repository = RepositoryFactory.Instance.CreateRepository<Repository>())
                {
                    UserAuthenticationData user = UserdataGenerator.CreateUserAuthentication();
                   
                    Customer customer = new Customer()
                    {
                        CustomerId = repository.Max<Customer, int>("CustomerId")+1,
                        Adress = adresse,
                        WerkstattKonzern = DEFAULTWERKSTATTKONZERN,
                        FullName = fullName,
                        BirthDate = birthDate,
                        Username = user.Username,
                        Password = user.Password
                    };

                    repository.SaveOrUpdate(customer);

                    return customer.Clone() as Customer;
                }

            }
            catch (DatabaseException )
            {
                throw;
            }
            catch (Exception ex)
            {
                throw (new DatabaseException(ex, "Error in creating customer!"));
            }

        }

        public static Customer GetSingleCustomerById(int id)
        {

            try
            {
                using (IRepository repository = RepositoryFactory.Instance.CreateRepository<Repository>())
                {
                    return repository.GetById<Customer>(id);
                }
            }
            catch (DatabaseException )
            {
                throw;
            }
            catch (Exception ex)
            {
                throw (new DatabaseException(ex, "Error in selecting singel customer by id "+id));
            }

        }

        public static Customer GetSingleCustomerByExpression(Expression<Func<Customer, bool>> expression)
        {

            try
            {
                using (IRepository repository = RepositoryFactory.Instance.CreateRepository<Repository>())
                {
                    return repository.SelectSingleWhere(expression);
                }
            }
            catch (DatabaseException )
            {
                throw;
            }
            catch (Exception ex)
            {
                throw (new DatabaseException(ex, ""));
            }

        }

        public static IEnumerable<Customer> GetAllCutomer()
        {

            try
            {
                IEnumerable<Customer> ret = null;
                using (IRepository repo = RepositoryFactory.Instance.CreateRepository<Repository>())
                {
                   ret = new List<Customer>(repo.SelectMany<Customer>().AsEnumerable<Customer>());
                   
                }
                return ret;
            }
            catch (DatabaseException )
            {
                throw;
            }
            catch (Exception ex)
            {
                throw (new DatabaseException(ex, "Error in UserManager selecting all customers" ));
            }

        }
        #endregion
    }
}
