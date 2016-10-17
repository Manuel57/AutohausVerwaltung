using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDM.Business.Entities
{
    public class PhotoEntity
    {
        #region Properties
        public int PhotoID { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public Guid RowGuid { get; set; }
        public DateTime ModifiedDate { get; set; }
        #endregion
    }
}
