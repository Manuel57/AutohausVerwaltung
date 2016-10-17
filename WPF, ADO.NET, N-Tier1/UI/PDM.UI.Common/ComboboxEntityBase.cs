using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDM.UI.Common
{
    public class ComboboxEntityBase<T> : ViewModelBase
    {
        #region Members
        private int id;
        private string name;
        #endregion

        #region Properties
        public int ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
                this.OnPropertyChanged("ID");
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
                this.OnPropertyChanged("Name");
            }
        }
        #endregion
    }
}
