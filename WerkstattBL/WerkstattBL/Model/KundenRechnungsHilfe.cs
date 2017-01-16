using Database.Common;
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerkstattBL.Model
{
    [Class(Table ="kundenrechhilfe",Name = "WerkstattBL.Model.KundenRechnungsHilfe,WerkstattBL.Model")]
    public class KundenRechnungsHilfe : IEntity
    {
        [CompositeId(1)]
        [KeyProperty(2, Name = "KundenID", Column = "KundenID", TypeType = typeof(int))]
        [KeyProperty(3, Name = "Rechnungsnummer", Column = "Rechnungsnummer", TypeType = typeof(int))]
        [Column(Name = "KundenID")]
        public virtual int KundenID { get; set; }

        [Column(Name = "Rechnungsnummer")]
        public virtual int Rechnungsnummer { get; set; }
        
        [Property(Name = "CustomMessage", Column ="CUSTOMMESSAGE")]
        public virtual string CustomMessage { get; set; }

        [Property(Column ="DATUM",Name ="Datum",TypeType =typeof(DateTime))]
        public virtual DateTime Datum { get; set; }
    }
}
