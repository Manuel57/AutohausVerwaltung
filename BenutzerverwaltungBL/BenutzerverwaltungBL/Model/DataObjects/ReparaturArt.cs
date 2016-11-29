using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenutzerverwaltungBL.Model.DataObjects
{
    [Class(Table ="Reparaturart",Name = "BenutzerverwaltungBL.Model.DataObjects.ReparaturArt,BenutzerverwaltungBL")]
   public class ReparaturArt
    {
        [Id(Name ="ReparaturArtId",Column ="Reparaturartid")]
        public virtual int ReparaturArtId { get; set; }

        [Property(Name ="Bezeichnung",Column ="bezeichnung")]
        public virtual string Bezeichnung { get; set; }

        [Property(Name ="Preis",Column ="Preis")]
        public virtual long Preis { get; set; }

        [Set(1, Name = "Reparaturen", Table = "Reparatur", Lazy = CollectionLazy.True, Fetch = CollectionFetchMode.Join)]
        [Key(2, Column = "RepartId")]
        [OneToMany(3, Class = "BenutzerverwaltungBL.Model.DataObjects.Reparatur,BenutzerverwaltungBL", ClassType = typeof(Reparatur))]
        public virtual ISet<Reparatur> Reparaturen { get; set; }

        /* [List(Name ="Teile", Table ="Reparaturteile")] 
         [Key(Column ="ReparaturArtId")]
         [ManyToMany(Column ="Autoteilbez",Class = "AutoTeile")]
         public virtual IList<AutoTeile> Teile { get; set; }*/

        public ReparaturArt() { }

        public override string ToString()
        {
            return this.Bezeichnung + " " + this.Preis;
        }
    }
}
