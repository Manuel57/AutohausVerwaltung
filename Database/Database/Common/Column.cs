// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2017-1-2</date>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Common
{
    public class Column
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public NHibernate.Type.IType Type { get; set; }

        public override int GetHashCode( )
        {
            return this.Name.GetHashCode();
        }
        public override bool Equals( object obj )
        {
            if ( obj == null )
                return false;
            return this.Name.Equals(( obj as Column ).Name);
        }
    }
}
