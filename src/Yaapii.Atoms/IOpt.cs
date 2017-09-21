using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms
{
    /// <summary>
    /// Represents an untyped Optional
    /// </summary>
    public interface IOpt
    {
        bool Has();
    }

    /// <summary>
    /// Represents an optional of the given type.
    /// This optional can be tested if there is value inside.
    /// It can also be asked if it has a value like this: if(someOpt is Opt.Empty)
    /// </summary>
    /// <typeparam name="T">The Encapsulated type</typeparam>
    public class IOpt<T> : IOpt
    {
        private readonly List<T> _item;

        private IOpt() : this(new List<T>()) { }

        /// <summary>
        /// Makes an opt with the given item inside.
        /// </summary>
        /// <param name="item">The item to encapsulate</param>
        public IOpt(T item) : this(new List<T>() { item }) { }

        private IOpt(List<T> items)
        {
            _item = items;
        }

        /// <summary>
        /// Delivers the encapsulated item
        /// </summary>
        /// <returns></returns>
        public T Out()
        {
            if (!Has()) throw new Exception("I am empty and therefore cannot deliver anything.");
            return _item[0];
        }

        /// <summary>
        /// Checks if there is an item inside
        /// </summary>
        /// <returns>True if there is an item</returns>
        public bool Has() { return _item.Count == 1; }

        /// <summary>
        /// Makes an empty optional of the given type
        /// </summary>
        public sealed class Empty : IOpt<T>
        {
            public Empty() : base() { }
        }
    }


}
