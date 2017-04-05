using Database.Common;
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerkstattBL.Model
{
    [Class(Table = "werkstattlager", Name = "WerkstattBL.Model.Werkstattlager,WerkstattBL", SelectBeforeUpdate = true, DynamicUpdate = true)]
    public class Werkstattlager : IEntity
    {       
        [CompositeId(1)]
        [KeyProperty(2, Column = "standort", Name = "Standort")]
        [KeyManyToOne(3, Column = "bezeichnung", Name = "Teil", Lazy = RestrictedLaziness.False, Class = "WerkstattBL.Model.Autoteile,WerkstattBL", ClassType = typeof(Autoteile))]

        [Column(Name = "standort")]
        public virtual string Standort { get; set; }

        [Column(Name = "bezeichnung")]
        public virtual Autoteile Teil { get; set; }
        

        [Property(Name = "Bestand", Column = "lagerbastand")]
        public virtual int Bestand { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as Werkstattlager;
            if (t == null)
                return false;
            if (Teil.Bezeichnung.Equals(t.Teil.Bezeichnung) && Standort.Equals(t.Standort))
                return true;
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
