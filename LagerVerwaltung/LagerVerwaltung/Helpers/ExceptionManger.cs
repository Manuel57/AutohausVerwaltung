using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Verwaltung.Exception;

namespace LagerVerwaltung.Helpers
{
    public class ExceptionManger : ExceptionHelper
    {
        private static ExceptionManger instance;

        public static ExceptionManger Instance
        {
            get
            {
                if (instance == null)
                    instance = new ExceptionManger();
                return instance;
            }
        }
        private ExceptionManger() { }

        public override void Handle(Exception e)
        {
           lock(this)
           {
                MessageBox.Show(this.CreateMessage(e), "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None);
            }
        }
    }
}
