﻿using Database.Common;
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerkstattBL.Model
{
    [Class(Table = "Autoteile", Name = "WerkstattBL.Model.Autoteile,WerkstattBL")]
    public class Autoteile : IEntity, ICloneable
    {
        [Id(Name = "Bezeichnung", Column = "Bezeichnung")]
        public virtual string Bezeichnung { get; set; }

        [Property(Name = "Preis", Column = "Preis")]
        public virtual double Preis { get; set; }

        [Set(Name = "Lager", Table = "werkstattlager", Cascade = "all")]
        [Key(Column = "bezeichnung")]
        [OneToMany(Class = "WerkstattBL.Model.Werkstattlager")]
        public virtual IEnumerable<Werkstattlager> Lager { get; set; }

       
        public virtual object Clone()
        {
            return new Autoteile() { Bezeichnung = this.Bezeichnung, Lager = this.Lager, Preis = this.Preis };
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            return this.Bezeichnung.Equals((obj as Autoteile).Bezeichnung);
        }
        public override int GetHashCode()
        {
            return this.Bezeichnung.GetHashCode();
        }
    }
}