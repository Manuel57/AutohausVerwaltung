using Database.Common;
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    [Class(Table ="mitarbeiter")]
    public class Employee : IEntity
    {
        [Property(Name = "Name" , Column = "ma_name")]
        public virtual string Name { get; set; }
        [Property(Name = "Pensum" , Column = "arbeitspensum")]
        public virtual int Pensum { get; set; }
        [Id(Name = "Mnr" , Column = "ma_nr")]
        public virtual int Mnr { get; set; }

        public override string ToString( )
        {
            return this.Name + "  " + this.Mnr + "   " + this.Pensum;
        }
    }
}
