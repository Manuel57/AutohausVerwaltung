using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDM.UI.Entities
{

    public class ProductSubCategoryEntity : UIEntityBase
    {
        #region Private Members
        private int productSubCategoryID;
        private string name;
        private Guid rowGuid;
        private DateTime modifiedDate;
        private int productCategoryID;
        private string categoryName;
        #endregion

        #region Public Properties
        public int ProductSubCategoryID
        {
            get { return this.productSubCategoryID; }
            set
            {
                this.productSubCategoryID = value;
                this.OnPropertyChanged("ProductSubCategoryID");
            }
        }
        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                this.OnPropertyChanged("Name");
            }
        }

        public virtual string CategoryName
        {
            get { return this.categoryName; }
            set
            {
                this.categoryName = value;
                this.OnPropertyChanged("CategoryName");
            }
        }
        public Guid RowGuid
        {
            get { return this.rowGuid; }
            set
            {
                this.rowGuid = value;
                this.OnPropertyChanged("RowGuid");
            }
        }
        public DateTime ModifiedDate
        {
            get { return this.modifiedDate; }
            set
            {
                this.modifiedDate = value;
                this.OnPropertyChanged("ModifiedDate");
            }
        }

        public int ProductCategoryID
        {
            get { return this.productCategoryID; }
            set
            {
                this.productCategoryID = value;
                this.OnPropertyChanged("ProductModelID");
            }
        }
        #endregion
    }
}
