// <copyright file="Database.Common.ObserverPattern.IObservable.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-12</date>
// </copyright>

namespace Database.Common.ObserverPattern
{
    /// <summary>
    /// interface IObservable
    /// </summary>
    public interface IObservable
    {
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);
    }
}