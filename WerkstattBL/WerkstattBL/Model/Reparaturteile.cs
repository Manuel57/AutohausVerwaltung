using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WerkstattBL.Model
{
    [Class(Table ="Reparaturteile",Name ="WerkstattBL.Model.Reparaturteile,WerkstattBL",SelectBeforeUpdate =true,DynamicUpdate =true)]
   public class Reparaturteile
    {
        [CompositeId(1)]
        [KeyManyToOne(2, Column = "autoteilbez", Name = "Teil", Lazy = RestrictedLaziness.False, Class = "WerkstattBL.Model.Autoteile,WerkstattBL", ClassType = typeof(Autoteile))]
        [KeyManyToOne(3, Column = "reparaturartid", Name = "RepArt", Lazy = RestrictedLaziness.False, Class = "WerkstattBL.Model.Reparaturart,WerkstattBL", ClassType = typeof(Reparaturart))]
        [Column(Name = "autoteilbez")]
        public virtual Autoteile Teil { get; set; }

        [Column(Name = "reparaturartid")]
        public virtual Reparaturart RepArt { get; set; }

        [Property(Name ="Menge",Column ="menge",TypeType =typeof(int))]
        public virtual int Menge { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as Reparaturteile;
            if (t == null)
                return false;
            if (Teil.Bezeichnung.Equals(t.Teil.Bezeichnung) && RepArt.ReparaturArtId == t.RepArt.ReparaturArtId)
                return true;
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
