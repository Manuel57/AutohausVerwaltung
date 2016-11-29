using Database.Common;
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenutzerverwaltungBL.Model.DataObjects
{
    [Class(Table ="Reparatur",Name = "BenutzerverwaltungBL.Model.DataObjects.Reparatur,BenutzerverwaltungBL")]
   public  class Reparatur : IEntity
    {
         [CompositeId(1)]
         [KeyProperty(2,Name = "ReparaturId",Column ="RepId",TypeType = typeof(int))]
         [KeyManyToOne(3,Name ="Rechnungsnummer", Column ="Rechnungsnummer", Class = "BenutzerverwaltungBL.Model.DataObjects.Rechnung, BenutzerverwaltungBL", ClassType =typeof(Rechnung),Lazy =RestrictedLaziness.Proxy)]
         [Column(Name ="RepId")]
         public virtual int ReparaturId { get; set; }

        [Column(Name ="RNR")]
        public virtual Rechnung Rechnungsnummer { get; set; }

        /* [ManyToOne(Class = "BenutzerverwaltungBL.Model.DataObjects.ReparaturArt", Column ="RepArtId",NotNull =true,Lazy =Laziness.False)]
         public virtual ReparaturArt RepArt { get; set; }*/
        [Property(Name ="RepArt",Column = "RepArtId")]
        public virtual int RepArt { get; set; }

        [Property(Name ="Standort",Column ="Standort",TypeType =typeof(string))]
        public virtual string Standort { get; set; }

        [Property(Name = "ReparaturDatum",Column ="Datum",TypeType =typeof(DateTime))]
        public virtual DateTime ReparaturDatum { get; set; }

        public Reparatur() { }

        public  override bool Equals(object obj)
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
