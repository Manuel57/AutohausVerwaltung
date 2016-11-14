using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.Attributes;
using Database.Common;

namespace BenutzerverwaltungBL.Model.DataObjects
{
    [Class(Table ="kunde")]
    public class Customer:ICloneable,IEntity
    {
        
        [Id(Column ="KundenId",Name ="CustomerId")]
        public virtual int CustomerId { get; set; }

        [Property(Column ="WerkstattKonzern",Lazy =false)] 
        public virtual string WerkstattKonzern { get; set; }

        [Property(Column = "Vorname",Name ="FullName")]
        public virtual string FullName { get; set; }

        [Property(Column = "GebDate",Name ="BirthDate")]
        public virtual DateTime BirthDate { get; set; }

        [Property(Column = "Adresse",Name ="Adress")]
        public virtual string Adress { get; set; }

        [Property(Column ="Username", Unique =true)]
        public virtual string Username { get; set; }

        [Property(Column ="Password")]
        public virtual string Password { get; set; }

        public Customer() { }
        public Customer(Customer c)
        {
            Adress = c.Adress;
            CustomerId = c.CustomerId;
            WerkstattKonzern = c.WerkstattKonzern;
            FullName = c.FullName;
            BirthDate = c.BirthDate;
            Username = c.Username;
            Password = c.Password;
        }

        public virtual object Clone()
        {
            return new Customer(this);
        }
    }
}
