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
    public class Werkstatt : IEntity
    {
        [Id(Name = "Standort" , Column = "standort")]
        public virtual string Standort { get; set; }

        [Set(Name = "Werkstattlager" , Table = "werkstattlager" , Cascade = "all")]
        [Key(Column = "standort")]

        [ManyToMany(Class = "LagerverwaltungBL.Model.Autoteile" , Column = "bezeichnung")]
        public virtual IEnumerable<Autoteile> Werkstattlager { get; set; }
    }
}
