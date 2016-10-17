using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDM.UI.Entities
{
    public class ProductEntity : UIEntityBase
    {
        #region Private Members
        private int productID;
        private string name;
        private string productNumber;
        private bool makeFlag;
        private string color;
        private decimal? standardCost;
        private decimal? listPrice;
        private string size;
        private decimal? weight;
        private string style;
        private DateTime? sellStartDate;
        private int productSubCategoryID;
        private Guid rowGuid;
        private DateTime modifiedDate;
        private int productModelID;
        private int photoID;
        private PhotoEntity photo;
        #endregion

        #region Public Properties
        public int ProductID
        {
            get
            {
                return this.productID;
            }
            set
            {
                this.productID = value;
                OnPropertyChanged("ProductID");
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                OnPropertyChanged("Name");
            }
        }


        public string ProductNumber
        {
            get
            {
                return this.productNumber;
            }
            set
            {
                this.productNumber = value;
                OnPropertyChanged("ProductNumber");
            }
        }


        public bool MakeFlag
        {
            get
            {
                return this.makeFlag;
            }
            set
            {
                this.makeFlag = value;
                OnPropertyChanged("MakeFlag");
            }
        }


        public string Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
                OnPropertyChanged("Color");
            }
        }


        public decimal? StandardCost
        {
            get
            {
                return this.standardCost;
            }
            set
            {
                this.standardCost = value;
                OnPropertyChanged("StandardCost");
            }
        }


        public decimal? ListPrice
        {
            get
            {
                return this.listPrice;
            }
            set
            {
                this.listPrice = value;
                OnPropertyChanged("ListPrice");
            }
        }


        public string Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
                OnPropertyChanged("Size");
            }
        }


        public decimal? Weight
        {
            get
            {
                return this.weight;
            }
            set
            {
                this.weight = value;
                OnPropertyChanged("Weight");
            }
        }


        public string Style
        {
            get
            {
                return this.style;
            }
            set
            {
                this.style = value;
                OnPropertyChanged("Style");
            }
        }


        public DateTime? SellStartDate
        {
            get
            {
                return this.sellStartDate;
            }
            set
            {
                this.sellStartDate = value;
                OnPropertyChanged("SellStartDate");
            }
        }


        public int ProductSubCategoryID
        {
            get
            {
                return this.productSubCategoryID;
            }
            set
            {
                this.productSubCategoryID = value;
                OnPropertyChanged("ProductSubCategoryID");
            }
        }


        public Guid RowGuid
        {
            get
            {
                return this.rowGuid;
            }
            set
            {
                this.rowGuid = value;
                OnPropertyChanged("RowGuid");
            }
        }


        public DateTime ModifiedDate
        {
            get
            {
                return this.modifiedDate;
            }
            set
            {
                this.modifiedDate = value;
                OnPropertyChanged("ModifiedDate");
            }
        }

        public int ProductModelID
        {
            get
            {
                return this.productModelID;
            }
            set
            {
                this.productModelID = value;
                OnPropertyChanged("ProductModelID");
            }
        }

        public int PhotoID
        {
            get
            {
                return this.photoID;
            }
            set
            {
                this.photoID = value;
                OnPropertyChanged("PhotoID");
            }
        }

        #endregion
    }
}
