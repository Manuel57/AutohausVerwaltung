// <copyright file="Benutzerverwaltung.Helpers.Extensions.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-12-7</date>
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Benutzerverwaltung.Helpers
{
    public static class Extensions
    {
        public static void ReadOnly(this TextBox tb)
        {
            tb.IsReadOnly = true;
        }

      
    }
}
