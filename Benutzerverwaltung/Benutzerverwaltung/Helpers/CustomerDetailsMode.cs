// <copyright file="Benutzerverwaltung.Helpers.CustomerDetailsMode.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-29</date>
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benutzerverwaltung.Helpers
{
    /// <summary>
    /// Mode of the customerdetails view
    /// </summary>
    public enum CustomerDetailsMode
    {
        /// <summary>
        /// view for deleting a customer
        /// </summary>
        Delete,

        /// <summary>
        /// view for editing a customer
        /// </summary>
        Details
    }
}
