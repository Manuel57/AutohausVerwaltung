using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.Attributes;

namespace BenutzerverwaltungBL.Model.DataObjects
{
    [Class(Table ="kunde")]
    public class Customer
    {
        //       KundenID        INTEGER,
        // WerkstattKonzern varchar2(50),
        //Vorname VARCHAR2(50),
        //GebDate         DATE,
        //   Password        VARCHAR2(200),
        //Username VARCHAR2(20) unique,
        [Id(Column ="KundenId")]
        public int CustomerId { get; set; }
        [Property(Column ="WerkstattKonzern")]
        public string WerkstattKonzern { get; set; }

        [Property(Column = "Vorname")]
        public int FullName { get; set; }
        [Property(Column = "GebDate")]
        public DateTime BirthDate { get; set; }
        [Property(Column ="Username", Unique =true)]
        public string Username { get; set; }
        [Property(Column ="Password")]
        public string Password { get; set; }
    }
}
