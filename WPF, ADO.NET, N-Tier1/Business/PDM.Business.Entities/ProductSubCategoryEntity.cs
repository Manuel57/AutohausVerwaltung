using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDM.Business.Entities
{
    public class ProductSubCategoryEntity
    {
        #region Public Properties
        public int ProductSubCategoryID { get; set; }
        public string Name { get; set; }
        public Guid RowGuid { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ProductCategoryID { get; set; }
        #endregion
    }
}
