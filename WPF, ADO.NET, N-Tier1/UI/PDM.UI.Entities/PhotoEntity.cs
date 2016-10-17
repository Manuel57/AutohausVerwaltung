using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDM.UI.Entities
{
    public class PhotoEntity : UIEntityBase
    {

        #region Private Members
        private int photoID;
        private string name;
        private byte[] image;
        private Guid rowGuid;
        private DateTime modifiedDate;
        #endregion

        #region Public Members
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
        public byte[] Image
        {
            get
            {
                return this.image;
            }
            set
            {
                this.image = value;
                OnPropertyChanged("Image");
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
        #endregion
    }
}
