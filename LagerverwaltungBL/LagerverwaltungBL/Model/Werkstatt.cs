using Database.Common;
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerverwaltungBL.Model
{
    [Class(Table = "Werkstatt" , Name = "LagerverwaltungBL.Model.Werkstatt,LagerverwaltungBL")]
    public class Werkstatt : SdoObject, IEntity
    {

        private string standort;
        [Id(Name = "Standort" , Column = "standort")]
        public virtual string Standort
        {
            get { return this.standort; }
            set { this.standort = value; this.Id = value; }
        }

        [Set(Name = "Lager" , Table = "werkstattlager" , Cascade = "all")]
        [Key(Column = "standort")]
        [OneToMany(Class = "LagerverwaltungBL.Model.Werkstattlager")]
        public virtual IEnumerable<Werkstattlager> Lager { get; set; }


    }
}
