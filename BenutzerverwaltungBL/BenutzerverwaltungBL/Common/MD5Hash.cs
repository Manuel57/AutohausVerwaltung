// <copyright file="BenutzerverwaltungBL.Common.md5hash.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-13</date>
// </copyright>

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BenutzerverwaltungBL.Common
{
    public static class MD5Hash
    {
        /// <summary>
        /// Gets the MD5 hash.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>MD5 hash of the input</returns>
        public static string Compute( string input )
        {
            MD5 hasher = MD5.Create();
            byte[] data = hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder stringBuilder = new StringBuilder();

            for ( int i = 0; i < data.Length; i++ )
            {
                stringBuilder.Append(data[i].ToString("x2" , CultureInfo.InvariantCulture));
            }

            return stringBuilder.ToString();
        }
    }
}
