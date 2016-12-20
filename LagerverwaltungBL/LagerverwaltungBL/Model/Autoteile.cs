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
    public class Autoteile : IEntity
    {
        [Id(Name = "Bezeichnung" , Column = "Bezeichnung")]
        public virtual string Bezeichnung { get; set; }
        [Property(Name = "Preis" , Column = "Preis")]
        public virtual double Preis { get; set; }
        [Set(Name = "Lager" , Table = "zentrallagerbestand" , Cascade = "all")]
        [Key(Column = "bezeichnung")]
        [ManyToMany(Class = "LagerverwaltungBL.Model.Zentrallager" , Column = "standort")]
        public virtual IEnumerable<Zentrallager> Lager { get; set; }

        [Set(Name = "Werkstattlager" , Table = "werkstattlager" , Cascade = "all")]
        [Key(Column = "bezeichnung")]
        [ManyToMany(Class = "LagerverwaltungBL.Model.Werkstatt" , Column = "standort")]
        public virtual IEnumerable<Zentrallager> Werkstattlager { get; set; }

        //[CompositeElement(Class = "LagerverwaltungBL.Model.Autoteile, LagerverwaltungBL")]
        
        [CompositeElement(Class = "LagerverwaltungBL.Model.Werkstattlager")]
        public virtual int Bestand { get; set; }
    }
}
