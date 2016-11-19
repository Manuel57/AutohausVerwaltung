using Database.Common;
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenutzerverwaltungBL.Model.DataObjects
{
    [Class(Table = "Rechnung",Name ="Rechnung")]
    public class Rechnung : IEntity, ICloneable
    {
        [Id(Name ="Rechnungsnummer",Column ="Rechnungsnummer")]
        public virtual int Rechnungsnummer { get; set; }

        [Property(Name = "Rechnungsdatum",Column ="Rdatum",Type= "date")]
        public virtual DateTime Rechnungsdatum { get; set; }

       [Property(Name = "Gesamtpreis",Column ="Gesamtpreis")]
        public virtual long Gesamtpreis { get; set; }

        [ManyToOne(Class ="Customer",Column ="KundenId",NotNull = true,Cascade = "save-update")]
        public virtual Customer KundenId { get; set; }

        [List(Name ="Reparaturen")]
        [Key(Column ="Rechnungsnummer")] 
        [OneToMany(Class ="Reparatur")]
        public virtual IList<Reparatur> Reparaturen { get; set; }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
