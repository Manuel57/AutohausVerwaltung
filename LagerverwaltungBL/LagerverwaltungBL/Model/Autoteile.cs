using Database.Common;
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagerverwaltungBL.Model
{
    [Class(Table = "Autoteile" , Name = "LagerverwaltungBL.Model.Autoteile,LagerverwaltungBL")]
    public class Autoteile : IEntity, ICloneable
    {
        [Id(Name = "Bezeichnung" , Column = "Bezeichnung")]
        public virtual string Bezeichnung { get; set; }

        [Property(Name = "Preis" , Column = "Preis")]
        public virtual double Preis { get; set; }

        [Set(Name = "Lager" , Table = "werkstattlager" , Cascade = "all")]
        [Key(Column = "bezeichnung")]
        [OneToMany(Class = "LagerverwaltungBL.Model.Werkstattlager")]
        public virtual IEnumerable<Werkstattlager> Lager { get; set; }

        //[Set(Name = "Werkstattlager" , Table = "werkstattlager" , Cascade = "all")]
        [Set(Name = "Zentrallager" , Table = "zentrallagerbestand" , Cascade = "all")]
        [Key(Column = "standort")]
        [ManyToMany(Class = "LagerverwaltungBL.Model.Zentrallager" , Column = "bezeichnung")]
        public virtual IEnumerable<Zentrallager> Zentrallager { get; set; }

        //public virtual Werkstattlager WerkstattLager { get; set; }
        public virtual object Clone( )
        {
            return new Autoteile() { Bezeichnung = this.Bezeichnung , Lager = this.Lager , Preis = this.Preis};
        }
    }
}
