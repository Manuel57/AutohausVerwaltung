﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Verwaltung
{
    public static class Extensions
    {

        public static void ReadOnly( this TextBox tb )
        {
            tb.IsReadOnly = true;
        }

    }
}
