using Database.Common;
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenutzerverwaltungBL.Model.DataObjects
{
    [Class(Table ="Reparatur",Name ="Reparatur")]
   public  class Reparatur : IEntity
    {
         [CompositeId(1)]
         [KeyProperty(2,Name = "ReparaturId",Column ="RepId",TypeType = typeof(long))]
         [KeyManyToOne(3,Name ="Rechnungsnummer", Column ="Rechnungsnummer", Class = "Rechnung",ClassType =typeof(Rechnung))]
         [Column(Name ="RepId")]
         public virtual long ReparaturId { get; set; }

        [Column(Name ="Rechnungsnummer")]
        public virtual Rechnung Rechnungsnummer { get; set; }

        [ManyToOne(Class ="ReparaturArt",Column ="RepArtId",NotNull =true,Lazy =Laziness.False)]
        public virtual ReparaturArt RepArt { get; set; }

        [Property(Name ="Standort",Column ="Standort",TypeType =typeof(string))]
        public virtual string Standort { get; set; }

        [Property(Name = "ReparaturDatum",Column ="Datum",TypeType =typeof(DateTime))]
        public virtual DateTime ReparaturDatum { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as Reparatur;
            if (t == null)
                return false;
            if (ReparaturId == t.ReparaturId && Rechnungsnummer == t.Rechnungsnummer)
                return true;
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
