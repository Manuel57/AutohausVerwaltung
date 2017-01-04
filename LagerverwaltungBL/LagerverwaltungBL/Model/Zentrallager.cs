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
    public class Zentrallager : SdoObject, IEntity
    {
        private string standort;
        [Id(Name = "Standort" , Column = "Standort")]
        public virtual string Standort
        {
            get { return standort; }
            set { this.standort = value; this.Id = value; }
        }
        [Set(Name = "Teile" , Table = "zentrallagerbestand" , Cascade = "all")]
        [Key(Column = "standort")]
        [ManyToMany(Class = "LagerverwaltungBL.Model.Autoteile" , Column = "bezeichnung")]
        public virtual IEnumerable<Autoteile> Teile { get; set; }
        //public virtual Point Coordinates { get; set; }

    }
}
