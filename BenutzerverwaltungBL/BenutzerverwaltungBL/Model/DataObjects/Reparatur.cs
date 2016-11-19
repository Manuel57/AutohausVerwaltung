using Database.Common;
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenutzerverwaltungBL.Model.DataObjects
{
    [Class(Table ="Reparatur")]
   public  class Reparatur : IEntity
    {
       //iwi mit compostie id den zusammengesetztn primary key
        public virtual int ReparaturId { get; set; }

        [KeyManyToOne(Class ="Rechnung",Column ="Rechnungsnummer",ForeignKey ="Rechnungsnummer")]
        public virtual int Rechnungsnummer { get; set; }

        public virtual int RepArtId { get; set; }
        public virtual string Standort { get; set; }
        public virtual DateTime ReparaturDatum { get; set; }
    }
}
