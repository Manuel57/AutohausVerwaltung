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

        [ManyToOne(Class = "BenutzerverwaltungBL.Model.DataObjects.Customer,BenutzerverwaltungBL", Name ="Kunde",Column ="KundenId",NotNull = true,Cascade = "delete")]
        public virtual Customer Kunde { get; set; }

        [Set(1,Name ="Reparaturen",Table = "Reparatur",Lazy = CollectionLazy.False,Fetch = CollectionFetchMode.Join,Cascade = "all-delete-orphan ")]
        [Key(2,Column ="RNR")] 
        [OneToMany(3,Class = "BenutzerverwaltungBL.Model.DataObjects.Reparatur,BenutzerverwaltungBL", ClassType =typeof(Reparatur))]
        public virtual ISet<Reparatur> Reparaturen { get; set; }

        [Property(Name = "isPdf", Column = "ausgestellt")]
        protected internal virtual int isPdf { get; set; }
        
        public virtual bool IsAlreadyPdf
        {
            get { return isPdf == 1; }
            set { isPdf = value? 1 : 0; }
        }
        

        public  Rechnung() { }

        public override string ToString()
        {
            return "Rechnungnummer: "+Rechnungsnummer+"\n"+
                     this.Kunde.FirstName + " " + this.Kunde.LastName + "\n"+
                    " vom " + Rechnungsdatum.ToShortDateString();
        }
    }
}
