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
        [Id(Column ="KundenId",Name ="CustomerId")]
        public int CustomerId { get; set; }

        [Property(Column ="WerkstattKonzern",Lazy =false)] 
        public virtual string WerkstattKonzern { get; set; }

        [Property(Column = "Vorname",Name ="FullName")]
        public virtual int FullName { get; set; }

        [Property(Column = "GebDate",Name ="BirthDate")]
        public virtual DateTime BirthDate { get; set; }

        [Property(Column = "Adresse",Name ="Adress")]
        public virtual string Adress { get; set; }

        [Property(Column ="Username", Unique =true)]
        public virtual string Username { get; set; }

        [Property(Column ="Password")]
        public virtual string Password { get; set; }
    }
}
