// <copyright file="BenutzerverwaltungBL.Controller.customermanager.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-13</date>
// </copyright>

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
    public static class CustomerManager
    {
        #region private fields
        private static string DEFAULTWERKSTATTKONZERN = "THE MECHANICS";
        private static IRepository repository = null;
        #endregion

        #region methods
        /// <summary>
        /// Creates a customer and inserts it into the database.
        /// Creates the random username and password for the customer.
        /// </summary>
        /// <param name="werkstattKonzern">name of the werkstattkonzern</param>
        /// <param name="fullName">the full name of the customer e.g. Huber Thomas</param>
        /// <param name="birthDate">the birth date of the customer</param>
        /// <param name="adresse">the adresse of the customer e.g. 9500 Villach Italienerstraße 3</param>
        /// <returns>a copie of the customer</returns>
        public static Customer CreateCustomer(string werkstattKonzern,string vorname,string nachname, DateTime birthDate,
                                        string adresse)
        {
            try
            {
                using (repository = RepositoryFactory.Instance.CreateRepository<Repository>())
                {
                    UserAuthenticationData user = UserdataGenerator.CreateUserAuthentication(vorname+nachname, birthDate);
                    Customer customer = new Customer()
                    {
                        CustomerId = repository.Max<Customer, int>("CustomerId")+1,
                        Adress = adresse,
                        WerkstattKonzern = DEFAULTWERKSTATTKONZERN,
                        FirstName = vorname,
                        LastName = nachname,
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

        /// <summary>
        /// returns the customer with the givn id from the database.
        /// throws an exception if an error occurs or more than one customer have got
        /// the given id.
        /// </summary>
        /// <param name="id">the id of the customer to select</param>
        /// <returns>the customer with the given id from the database</returns>
        public static Customer GetSingleCustomerById(int id)
        {
            try
            {               
                using (repository = RepositoryFactory.Instance.CreateRepository<Repository>())
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

        /// <summary>
        /// returns the customer matching the given
        /// linq expression.
        /// throws an exception if an error occurs.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>the customer matching the given expression</returns>
        public static Customer GetSingleCustomerByExpression(Expression<Func<Customer, bool>> expression)
        {
            try
            {
                using (repository = RepositoryFactory.Instance.CreateRepository<Repository>())
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

        /// <summary>
        /// returns all customers from the database.
        /// throws an exception if an error occurs.
        /// </summary>
        /// <returns> a IEnumerable of all customers</returns>
        public static IEnumerable<Customer> GetAllCustomers()
        {
            try
            {
                IEnumerable<Customer> ret = null;
                using (repository = RepositoryFactory.Instance.CreateRepository<Repository>())
                {
                    ret = new List<Customer>(repository.SelectMany<Customer>().AsEnumerable());

                   
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
        
        /// <summary>
        /// returns all customers matching the given expression.
        /// throws an exception if an error occurs.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>IEnumerable of all customers mathcing the expression given</returns>
        public static IEnumerable<Customer>GetAllCustomersWhere(Expression<Func<Customer, bool>> expression)
        {
            try
            {
                IEnumerable<Customer> ret = null;
                using (repository = RepositoryFactory.Instance.CreateRepository<Repository>())
                {
                    ret = new List<Customer>(repository.SelectManyWhere(expression));
                }
                return ret;
            }
            catch (DatabaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw (new DatabaseException(ex, "Error in selecting customers with linq expression!"));
            }

        }

        /// <summary>
        /// returns all customers matching the given criteria.
        /// throws an exception if an error occurs.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>IEnumerable of all customers mathcing the criteria given</returns>
        public static IEnumerable<Customer> GetAllCustomersWhere(DetachedCriteria criteria)
        {
            try
            {
                IEnumerable<Customer> ret = null;
                using (repository = RepositoryFactory.Instance.CreateRepository<Repository>())
                {
                    ret = new List<Customer>(repository.SelectManyWhere<Customer>(criteria));
                }
                return ret;
            }
            catch (DatabaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw (new DatabaseException(ex, "Error in selecting customers with criteria!"));
            }

        }


        /// <summary>
        /// updates all changes in the given customer to the database.
        /// throws an exception if an error occurs.
        /// </summary>
        /// <param name="newCustomer"></param>
        /// <returns>true if succeeded or throws an exception</returns>
        public static bool UpdateCustomer(Customer newCustomer)
        {
            try
            {
                using (repository = RepositoryFactory.Instance.CreateRepository<Repository>())
                {
                    repository.SaveOrUpdate(newCustomer);
                }
                return true;
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
        /// deletes the given customer in the database and all bills of the customer.
        /// throws an exception if an error occurs.
        /// </summary>
        /// <param name="customerToDelete"></param>
        /// <returns>true if succeeded or throws an exception</returns>
        public static bool DeleteCustomer(Customer customerToDelete)
        {
            try
            {
                using (repository = RepositoryFactory.Instance.CreateRepository<Repository>())
                {
                    customerToDelete.Rechnungen.ToList()
                       .ForEach(item => item.Reparaturen.ToList()
                       .ForEach(i => repository.Delete(i)
                       ));

                    repository.Delete(customerToDelete);
                }
                return true;
            }
            catch (DatabaseException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw (new DatabaseException(ex, "Error in CustomerManager deleting customer "+customerToDelete.FirstName));
            }

        }
        #endregion
    }
}
