using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.Attributes;
using Database.Common;

namespace BenutzerverwaltungBL.Model.DataObjects
{
    [Class(Table ="Kunde",Name = "BenutzerverwaltungBL.Model.DataObjects.Customer,BenutzerverwaltungBL")]
    public class Customer: ICloneable,IEntity
    {
        
        [Id(Column ="KundenId",Name ="CustomerId")]
        public virtual int CustomerId { get; set; }

        [Property(Column ="WerkstattKonzern",Lazy =false,Name = "WerkstattKonzern")] 
        public virtual string WerkstattKonzern { get; set; }

        [Property(Column = "Vorname",Name = "FirstName")]
        public virtual string FirstName { get; set; }

        // [Property(Column = "Nachname", Name = "LastName")]
        public virtual string LastName { get; set; } = "test";

        [Property(Column = "GebDate",Name ="BirthDate",TypeType =typeof(DateTime))]
        public virtual DateTime BirthDate { get; set; }

        [Property(Column = "Adresse",Name ="Adress")]
        public virtual string Adress { get; set; }

        [Property(Column ="Username", Unique =true,Name ="Username")]
        public virtual string Username { get; set; }

        [Property(Column ="Password", Name ="Password")]
        public virtual string Password { get; set; }

        [Set(1,Name ="Rechnungen", Lazy = CollectionLazy.False,Table ="Rechnung",Cascade ="all")]
        [Key(2,Column = "KundenId")]
        [OneToMany(3,Class = "BenutzerverwaltungBL.Model.DataObjects.Rechnung,BenutzerverwaltungBL", ClassType =typeof(Rechnung))]
        public virtual ISet<Rechnung> Rechnungen { get; set; }

        public Customer() { }
        public Customer(Customer c)
        {
            Adress = c.Adress;
            CustomerId = c.CustomerId;
            WerkstattKonzern = c.WerkstattKonzern;
            FirstName = c.FirstName;
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
