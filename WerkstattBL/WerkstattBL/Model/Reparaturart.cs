using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerkstattBL.Model
{
    [Class(Table = "Reparaturart", Name = "WerkstattBL.Model.Reparaturart,WerkstattBL")]
    public class Reparaturart
    {
        [Id(Name = "ReparaturArtId", Column = "Reparaturartid")]
        public virtual int ReparaturArtId { get; set; }

        [Property(Name = "Bezeichnung", Column = "bezeichnung")]
        public virtual string Bezeichnung { get; set; }

        [Property(Name = "Preis", Column = "Preis")]
        public virtual double Preis { get; set; }
               
        public Reparaturart() { }

        public override string ToString()
        {
            return this.Bezeichnung + " " + this.Preis;
        }
    }
}
