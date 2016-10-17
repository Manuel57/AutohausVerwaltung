using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDM.UI.Entities
{
    public class ProductModelEntity : UIEntityBase
    {
        #region Private Members
        private int productModelID;
        private string name;
        private Guid rowGuid;
        private DateTime modifiedDate;
        #endregion

        #region Public Properties
        public int ProductModelID
        {
            get { return this.productModelID; }
            set { this.productModelID = value;
            this.OnPropertyChanged("ProductModelID");
            }
        }
        public string Name
        {
            get { return this.name; }
            set { this.name = value;
            this.OnPropertyChanged("Name");
            }
        }
        public Guid RowGuid
        {
            get { return this.rowGuid; }
            set { this.rowGuid = value;
            this.OnPropertyChanged("RowGuid");
            }
        }
        public DateTime ModifiedDate
        {
            get { return this.modifiedDate; }
            set { this.modifiedDate = value;
            this.OnPropertyChanged("ModifiedDate");
            }
        }
        #endregion
    }
}
