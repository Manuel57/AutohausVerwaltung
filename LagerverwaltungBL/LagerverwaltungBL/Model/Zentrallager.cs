using Database.Common;
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerverwaltungBL.Model
{
    [Class(Table = "Zentrallager" , Name = "LagerverwaltungBL.Model.Zentrallager,LagerverwaltungBL")]
    public class Zentrallager : IEntity
    {
        [Id(Name = "Standort" , Column = "Standort")]
        public virtual string Standort { get; set; }
        [Set(Name = "Teile" , Table = "zentrallagerbestand" , Cascade = "all")]
        [Key(Column = "standort")]
        [ManyToMany(Class = "LagerverwaltungBL.Model.Autoteile" , Column = "bezeichnung")]
        public virtual IEnumerable<Autoteile> Teile { get; set; }
        public virtual Point Coordinates { get; set; }
    }
}
