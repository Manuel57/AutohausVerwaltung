// <copyright file="Database.Common.ObserverPattern.subject.cs">
// Copyright (c) 2016 All Rights Reserved
// <author>Manuel Lackenbucher</author>
// <author>Thomas Huber</author>
// <date>2016-11-12</date>
// </copyright>

using System;
using System.Collections.Generic;

namespace Database.Common.ObserverPattern
{
    public abstract class Subject : IObservable
    {
        /// <summary>
        /// List of observers
        /// </summary>
        private List<IObserver> observers = new List<IObserver>();

        /// <summary>
        /// Adds an observer to the list
        /// </summary>
        /// <param name="observer"></param>
        public void Subscribe(IObserver observer)
        {
            observers.Add(observer);
        }

        /// <summary>
        /// Removes an observer form the list
        /// </summary>
        /// <param name="observer"></param>
        public void Unsubscribe(IObserver observer)
        {
            observers.Remove(observer);
        }

        /// <summary>
        /// Calls the Update of each observer
        /// </summary>
        protected void Notify()
        {
            observers.ForEach(x => x.Update(this));
        }

    }
}
