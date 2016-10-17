using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDM.Business.Entities
{
    public class ProductEntity
    {
        #region Properties
        public int ProductID { get; set; }

        public string Name { get; set; }

        public string ProductNumber { get; set; }

        public bool MakeFlag { get; set; }

        public string Color { get; set; }

        public decimal? StandardCost { get; set; }

        public decimal? ListPrice { get; set; }

        public string Size { get; set; }

        public decimal? Weight { get; set; }

        public string Style { get; set; }

        public DateTime? SellStartDate { get; set; }

        public int ProductSubCategoryID { get; set; }

        public Guid RowGuid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public int ProductModelID { get; set; }

        public int PhotoID { get; set; }

        #endregion
    }
}
