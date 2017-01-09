using Database.Common;
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerkstattBL.Model
{
    [Class(Table = "Reparatur", Name = "WerkstattBL.Model.Reparatur,WerkstattBL")]
    public class Reparatur : IEntity
    {
        [CompositeId(1)]
        [KeyProperty(2, Name = "ReparaturId", Column = "RepId", TypeType = typeof(int))]
        [KeyProperty(3, Name = "Rechnungsnummer", Column = "RNR", TypeType = typeof(int))]
        [Column(Name = "RepId")]
        public virtual int ReparaturId { get; set; }

        [Column(Name = "RNR")]
        public virtual int Rechnungsnummer { get; set; }

        [ManyToOne(Class = "WerkstattBL.Model.Reparaturart,WerkstattBL", Column = "RepArtId", NotNull = true, Cascade = "none")]
        public virtual Reparaturart RepArt { get; set; }


        [Property(Name = "Standort", Column = "Standort", TypeType = typeof(string))]
        public virtual string Standort { get; set; }

        [Property(Name = "ReparaturDatum", Column = "Datum", TypeType = typeof(DateTime))]
        public virtual DateTime ReparaturDatum { get; set; }

        public Reparatur() { }

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
