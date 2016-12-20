using Database.Common;
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerverwaltungBL.Model
{
    [Class(Table = "werkstattlager" , Name = "LagerverwaltungBL.Model.Werkstattlager,LagerverwaltungBL")]
    public class Werkstattlager : IEntity
    {
        [Property(Name = "Bestand" , Column = "lagerbastand")]
        public virtual int Bestand { get; set; }

        [CompositeId(1)]
        [KeyManyToOne(2, Column ="bezeichnung", Name ="Teil", Lazy = RestrictedLaziness.False , Class = "LagerverwaltungBL.Model.Autoteile,LagerverwaltungBL" , ClassType = typeof(Autoteile))]
        [KeyManyToOne(3 , Column = "standort" , Name ="Lager" , Lazy = RestrictedLaziness.False , Class = "LagerverwaltungBL.Model.Werkstatt,LagerverwaltungBL" , ClassType = typeof(Werkstatt))]
        [Column(Name = "bezeichnung")]
        public virtual Autoteile Teil { get; set; }

        [Column(Name ="standort")]
        public virtual Werkstatt Lager { get; set; }

        public override bool Equals( object obj )
        {
            if ( obj == null )
                return false;
            var t = obj as Werkstattlager;
            if ( t == null )
                return false;
            if ( Teil == t.Teil && Lager == t.Lager )
                return true;
            return false;
        }
        public override int GetHashCode( )
        {
            return base.GetHashCode();
        }
    }
}
