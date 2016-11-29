using Database.Common;
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenutzerverwaltungBL.Model.DataObjects
{
    [Class(Table = "Rechnung",Name = "BenutzerverwaltungBL.Model.DataObjects.Rechnung,BenutzerverwaltungBL")]
    public class Rechnung : IEntity
    {
        [Id(Name ="Rechnungsnummer",Column ="Rechnungsnummer")]
        public virtual int Rechnungsnummer { get; set; }

        [Property(Name = "Rechnungsdatum",Column ="Rdatum",TypeType= typeof(DateTime))]
        public virtual DateTime Rechnungsdatum { get; set; }

       [Property(Name = "Gesamtpreis",Column ="Gesamtpreis")]
        public virtual long Gesamtpreis { get; set; }

        [ManyToOne(Class = "BenutzerverwaltungBL.Model.DataObjects.Customer,BenutzerverwaltungBL", Name ="Kunde",Column ="KundenId",NotNull = true,Cascade = "save-update")]
        public virtual Customer Kunde { get; set; }

        [Set(1,Name ="Reparaturen",Table = "Reparatur",Lazy = CollectionLazy.False,Fetch =CollectionFetchMode.Join)]
        [Key(2,Column ="Rechnungsnummer")] 
        [OneToMany(3,Class = "BenutzerverwaltungBL.Model.DataObjects.Reparatur,BenutzerverwaltungBL", ClassType =typeof(Reparatur))]
        public virtual ISet<Reparatur> Reparaturen { get; set; }

       public  Rechnung() { }
    }
}
