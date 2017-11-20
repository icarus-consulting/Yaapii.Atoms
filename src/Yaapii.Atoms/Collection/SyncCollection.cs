using System;
using System.Reflection;
using Yaapii.Atoms.Text;
using System.Collections.Generic;
using System.Collections;

namespace Yaapii.Atoms.Collection
{
    /// <summary>
    /// A collection which is threadsafe.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SyncCollection<T> : ICollection<T>
    {
        /// <summary>
        /// List to protect
        /// </summary>
        private readonly List<T> items;

        /// <summary>
        /// Lock object
        /// </summary>
        private readonly object sync;

        /// <summary>
        /// ctor
        /// </summary>
        public SyncCollection()
        {
            this.items = new List<T>();
            this.sync = new Object();
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="syncRoot"></param>
        public SyncCollection(object syncRoot)
        {
            if (syncRoot == null)
                throw new ArgumentNullException("syncRoot");

            this.items = new List<T>();
            this.sync = syncRoot;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="syncRoot">root object to sync</param>
        /// <param name="list">list to instantiate from</param>
        public SyncCollection(object syncRoot, IEnumerable<T> list)
        {
            if (syncRoot == null)
                throw new ArgumentNullException("syncRoot");
            if (list == null)
                throw new ArgumentNullException("list");

            this.items = new List<T>(list);
            this.sync = syncRoot;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="syncRoot">root object to sync</param>
        /// <param name="list">list to instantiate from</param>
        public SyncCollection(object syncRoot, params T[] list)
        {
            if (syncRoot == null)
                throw new ArgumentNullException("syncRoot");
            if (list == null)
                throw new ArgumentNullException("list");

            this.items = new List<T>(list.Length);
            for (int i = 0; i < list.Length; i++)
                this.items.Add(list[i]);

            this.sync = syncRoot;
        }

        /// <summary>
        /// Count items
        /// </summary>
        public int Count
        {
            get { lock (this.sync) { return this.items.Count; } }
        }

        /// <summary>
        /// The items
        /// </summary>
        protected List<T> Items
        {
            get { return this.items; }
        }

        /// <summary>
        /// Sync object
        /// </summary>
        public object SyncRoot
        {
            get { return this.sync; }
        }

        /// <summary>
        /// Access items by index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                lock (this.sync)
                {
                    return this.items[index];
                }
            }
            set
            {
                lock (this.sync)
                {
                    if (index < 0 || index >= this.items.Count)
                        throw new ArgumentOutOfRangeException("index", index, $"value {index} must be in range of {this.Items.Count}");

                    this.SetItem(index, value);
                }
            }
        }

        /// <summary>
        /// Add an item
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            lock (this.sync)
            {
                int index = this.items.Count;
                this.InsertItem(index, item);
            }
        }

        /// <summary>
        /// Clear the list
        /// </summary>
        public void Clear()
        {
            lock (this.sync)
            {
                this.ClearItems();
            }
        }

        /// <summary>
        /// Copy this to an array, starting with given index
        /// </summary>
        /// <param name="array">target array</param>
        /// <param name="index">index to start with</param>
        public void CopyTo(T[] array, int index)
        {
            lock (this.sync)
            {
                this.items.CopyTo(array, index);
            }
        }

        /// <summary>
        /// Determines whether this list contains the given item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            lock (this.sync)
            {
                return this.items.Contains(item);
            }
        }

        /// <summary>
        /// An enumerator to iterate the list
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            lock (this.sync)
            {
                return this.items.GetEnumerator();
            }
        }

        /// <summary>
        /// Index of an item
        /// </summary>
        /// <param name="item">item to search</param>
        /// <returns>the index</returns>
        public int IndexOf(T item)
        {
            lock (this.sync)
            {
                return this.InternalIndexOf(item);
            }
        }

        /// <summary>
        /// Insert an item at given position
        /// </summary>
        /// <param name="index">the position</param>
        /// <param name="item">the item to insert</param>
        public void Insert(int index, T item)
        {
            lock (this.sync)
            {
                if (index < 0 || index > this.items.Count)
                    throw new ArgumentOutOfRangeException(
                        "index", index,
                        new FormattedText(
                            "value {0} must be in range of {1}", index, this.Items.Count).AsString());

                this.InsertItem(index, item);
            }
        }

        /// <summary>
        /// Internal IndexOf
        /// </summary>
        /// <param name="item">item to search</param>
        /// <returns>the index</returns>
        int InternalIndexOf(T item)
        {
            int count = items.Count;

            for (int i = 0; i < count; i++)
            {
                if (object.Equals(items[i], item))
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Removes an item
        /// </summary>
        /// <param name="item">item to remove</param>
        /// <returns>true if success, false if item wasnt found</returns>
        public bool Remove(T item)
        {
            lock (this.sync)
            {
                int index = this.InternalIndexOf(item);
                if (index < 0)
                    return false;

                this.RemoveItem(index);
                return true;
            }
        }

        /// <summary>
        /// Remove a given index
        /// </summary>
        /// <param name="index">the index</param>
        public void RemoveAt(int index)
        {
            lock (this.sync)
            {
                if (index < 0 || index >= this.items.Count)
                    throw new ArgumentOutOfRangeException("index", index, $"value {index} must be in range of {this.Items.Count}");


                this.RemoveItem(index);
            }
        }

        /// <summary>
        /// Clears all items
        /// </summary>
        protected virtual void ClearItems()
        {
            this.items.Clear();
        }

        /// <summary>
        /// Insert an item at given index
        /// </summary>
        /// <param name="index">index to insert at</param>
        /// <param name="item">item to insert</param>
        protected virtual void InsertItem(int index, T item)
        {
            this.items.Insert(index, item);
        }

        /// <summary>
        /// Removes an item at given index.
        /// </summary>
        /// <param name="index">index to remove at</param>
        protected virtual void RemoveItem(int index)
        {
            this.items.RemoveAt(index);
        }

        /// <summary>
        /// Replaces an item at the given index.
        /// </summary>
        /// <param name="index">index to replace at</param>
        /// <param name="item">replacement item</param>
        protected virtual void SetItem(int index, T item)
        {
            this.items[index] = item;
        }

        /// <summary>
        /// This collection is never readonly.
        /// </summary>
        bool ICollection<T>.IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// This collection is always synchronized.
        /// </summary>
        bool IsSynchronized
        {
            get { return true; }
        }

        /// <summary>
        /// This is always writable
        /// </summary>
        bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// This is not fixed size
        /// </summary>
        bool IsFixedSize
        {
            get { return false; }
        }

        /// <summary>
        /// Internal add method
        /// </summary>
        /// <param name="value">value to add</param>
        /// <returns>new size</returns>
        int Add(object value)
        {
            VerifyValueType(value);

            lock (this.sync)
            {
                this.Add((T)value);
                return this.Count - 1;
            }
        }

        /// <summary>
        /// Test if contains object
        /// </summary>
        /// <param name="value">value to search</param>
        /// <returns>true if this list contains the value</returns>
        bool Contains(object value)
        {
            VerifyValueType(value);
            return this.Contains((T)value);
        }

        /// <summary>
        /// Index of given value
        /// </summary>
        /// <param name="value">the value</param>
        /// <returns>the index or -1 if not found</returns>
        int IndexOf(object value)
        {
            VerifyValueType(value);
            return this.IndexOf((T)value);
        }

        /// <summary>
        /// Insert ad given index
        /// </summary>
        /// <param name="index">index to insert at</param>
        /// <param name="value">value to insert</param>
        void Insert(int index, object value)
        {
            VerifyValueType(value);
            this.Insert(index, (T)value);
        }

        /// <summary>
        /// Removes an item
        /// </summary>
        /// <param name="value">value to remove</param>
        void Remove(object value)
        {
            VerifyValueType(value);
            this.Remove((T)value);
        }

        /// <summary>
        /// Test the value type
        /// </summary>
        /// <param name="value">the value to verify</param>
        static void VerifyValueType(object value)
        {
            if (value == null)
            {
                if (typeof(T).GetTypeInfo().IsValueType)
                {
                    throw new ArgumentException("value is null and a value type");
                }
            }
            else if (!(value is T))
            {
                throw
                    new ArgumentException(
                        new FormattedText(
                            "object is of type {0} but collection is of {1}",
                            value.GetType().FullName,
                            typeof(T).FullName).AsString());
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
