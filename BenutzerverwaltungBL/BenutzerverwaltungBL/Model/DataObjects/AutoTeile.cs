using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenutzerverwaltungBL.Model.DataObjects
{
    [Class (Table ="Autoteile")]
    public class AutoTeile
    {
        [Id (Name ="Bezeichnung", Column ="Bezeichnung",Type = "string")]
        public virtual string Bezeichnung { get; set; }

        [Property(Name ="Preis",Column ="Preis")]
        public virtual long Preis { get; set; }

        public AutoTeile() { }
    }
}
