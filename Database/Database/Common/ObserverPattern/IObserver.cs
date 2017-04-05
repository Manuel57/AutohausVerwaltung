// <copyright file="Database.Common.ObserverPattern.iobserver.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-12</date>
// </copyright>

using System;

namespace Database.Common.ObserverPattern
{
    public interface IObserver
    {
        void Update(object sender);
    }
}
