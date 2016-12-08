// <copyright file="Benutzerverwaltung.Helpers.SettingsManager.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-12-8</date>
// </copyright>

using BenutzerverwaltungBL.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benutzerverwaltung.Helpers
{
  
    public class SettingsManager
    {
        public static void ShowDialog()
        {
            SettingsDialog dlg = new SettingsDialog();
            if ( dlg.ShowDialog()==true )
            {
                ConfigureBl.UpdateSettings(dlg.Settings);
            }
        }
    }
}
