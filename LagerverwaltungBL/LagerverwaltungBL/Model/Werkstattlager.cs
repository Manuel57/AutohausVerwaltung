using Database.Common;
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerverwaltungBL.Model
{
    //[Class(Table = "werkstattlager" , Name = "LagerverwaltungBL.Model.Werkstattlager,LagerverwaltungBL")]
    //public class Werkstattlager : IEntity
    //{
    //    [Property(Name = "Bestand" , Column = "lagerbastand")]
    //    public virtual int Bestand { get; set; }

    //    [Set(1 , Name = "Rechnungen" , Lazy = CollectionLazy.False , Table = "Rechnung" , Cascade = "all")]
    //    [Key(2 , Column = "KundenId")]
    //    [OneToMany(3 , Class = "LagerverwaltungBL.Model.Autoteile,LagerverwaltungBL" , ClassType = typeof(Autoteile))]
    //    public virtual Autoteile Teile { get; set; }
    //}
}
