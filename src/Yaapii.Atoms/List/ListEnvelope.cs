using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Yaapii.Atoms.Fail;
using Yaapii.Atoms.Scalar;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword

namespace Yaapii.Atoms.List
{
    /// <summary>
    /// List envelope. Can make a readonly list from a scalar.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListEnvelope<T> : List<T>
    {
        private readonly IScalar<List<T>> _lst;
        private readonly UnsupportedOperationException _readonly = new UnsupportedOperationException("The list is readonly.");

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fnc">Function delivering a <see cref="List{T}"/></param>
        public ListEnvelope(Func<List<T>> fnc) : this(new ScalarOf<List<T>>(fnc))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="sc"></param>
        public ListEnvelope(IScalar<List<T>> sc)
        {
            _lst = sc;
        }

        /// <summary>
        /// access items
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                return base[index];
            }
            set
            {
                base[index] = value;
            }
        }

        /// <summary>
        /// Count elements
        /// </summary>
        public int Count { get { return base.Count; } }

        /// <summary>
        /// Capacity
        /// </summary>
        public int Capacity
        {
            get { return base.Capacity; }
            set { throw this._readonly; }
        }

        /// <summary>
        /// Not supported.
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item) { throw this._readonly; }

        /// <summary>
        /// Not supported.
        /// </summary>
        /// <param name="collection"></param>
        public void AddRange(IEnumerable<T> collection) { throw this._readonly; }
        
        /// <summary>
        /// This already is readonly.
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<T> AsReadOnly()
        {
            return new ReadOnlyCollection<T>(this);
        }
        
        /// <summary>
        /// Search 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int BinarySearch(T item)
        {
            return base.BinarySearch(item);
        }

        /// <summary>
        /// Binary search with the given comparer
        /// </summary>
        /// <param name="item"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public int BinarySearch(T item, IComparer<T> comparer)
        {
            return base.BinarySearch(item, comparer);
        }
        
        /// <summary>
        /// Binary search a limited range with the given comparer
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <param name="item"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
        {
            return base.BinarySearch(index, count, item, comparer);
        }

        /// <summary>
        /// Unsupported.
        /// </summary>
        public void Clear()
        {
            throw this._readonly;
        }
        
        /// <summary>
        /// Test if containing the given item
        /// </summary>
        /// <param name="item">Item to find</param>
        /// <returns>true if item is found</returns>
        public bool Contains(T item)
        {
            return base.Contains(item);
        }
        
        /// <summary>
        /// copy to a target array
        /// </summary>
        /// <param name="index">read start index</param>
        /// <param name="array">target array</param>
        /// <param name="arrayIndex">write start index</param>
        /// <param name="count">amount of elements to copy</param>
        public void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            base.CopyTo(index, array, arrayIndex, count);
        }
        
        /// <summary>
        /// copy to a target array
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            base.CopyTo(array, arrayIndex);
        }
        
        /// <summary>
        /// copy to a target array
        /// </summary>
        /// <param name="array"></param>
        public void CopyTo(T[] array)
        {
            base.CopyTo(array);
        }
        
        /// <summary>
        /// test if item exists
        /// </summary>
        /// <param name="match">matcher</param>
        /// <returns>true if item exists</returns>
        public bool Exists(Predicate<T> match)
        {
            return base.Exists(match);
        }

        /// <summary>
        /// find item
        /// </summary>
        /// <param name="match">matcher</param>
        /// <returns>item when found</returns>
        public T Find(Predicate<T> match)
        {
            return base.Find(match);
        }
        
        /// <summary>
        /// find all matching items
        /// </summary>
        /// <param name="match">matcher</param>
        /// <returns>matching items</returns>
        public List<T> FindAll(Predicate<T> match)
        {
            return base.FindAll(match);
        }

        /// <summary>
        /// Find matching item's index
        /// </summary>
        /// <param name="startIndex">index to start</param>
        /// <param name="count">how many to search through</param>
        /// <param name="match">matcher</param>
        /// <returns>found index</returns>
        public int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            return base.FindIndex(startIndex, count, match);
        }
        
        /// <summary>
        /// Find matching item's index
        /// </summary>
        /// <param name="startIndex">start index to search</param>
        /// <param name="match">matcher</param>
        /// <returns>index of matching item</returns>
        public int FindIndex(int startIndex, Predicate<T> match)
        {
            return base.FindIndex(startIndex, match);
        }
        
        /// <summary>
        /// Find matching item's index
        /// </summary>
        /// <param name="match">matcher</param>
        /// <returns>found index</returns>
        public int FindIndex(Predicate<T> match)
        {
            return base.FindIndex(match);
        }
        
        /// <summary>
        /// find last match
        /// </summary>
        /// <param name="match">matcher</param>
        /// <returns>last matching item</returns>
        public T FindLast(Predicate<T> match)
        {
            return base.FindLast(match);
        }
        
        /// <summary>
        /// Find index of last matching item.
        /// </summary>
        /// <param name="startIndex">start index to search</param>
        /// <param name="count">how many to find</param>
        /// <param name="match">matcher</param>
        /// <returns>found index</returns>
        public int FindLastIndex(int startIndex, int count, Predicate<T> match)
        {
            return base.FindLastIndex(startIndex, count, match);
        }

        /// <summary>
        /// Find index of last matching item.
        /// </summary>
        /// <param name="startIndex">start index to search</param>
        /// <param name="count">how many to find</param>
        /// <returns>found index</returns>
        public int FindLastIndex(int startIndex, Predicate<T> match)
        {
            return base.FindLastIndex(startIndex, match);
        }

        /// <summary>
        /// Find index of last matching item.
        /// </summary>
        /// <param name="match">matcher</param>
        /// <returns>found index</returns>
        public int FindLastIndex(Predicate<T> match)
        {
            return base.FindLastIndex(match);
        }
        
        /// <summary>
        /// Perform an action on each element.
        /// </summary>
        /// <param name="action">Action to perform</param>
        public void ForEach(Action<T> action)
        {
            base.ForEach(action);
        }
        
        /// <summary>
        /// Enumerator for this list.
        /// </summary>
        /// <returns>Enumerator</returns>
        public Enumerator GetEnumerator()
        {
            return base.GetEnumerator();
        }
        
        /// <summary>
        /// Range from this list
        /// </summary>
        /// <param name="index">Start index</param>
        /// <param name="count">Items count</param>
        /// <returns></returns>
        public List<T> GetRange(int index, int count)
        {
            return base.GetRange(index, count);
        }
        
        /// <summary>
        /// Index of given item
        /// </summary>
        /// <param name="item">item</param>
        /// <param name="index">index</param>
        /// <param name="count">count</param>
        /// <returns></returns>
        public int IndexOf(T item, int index, int count)
        {
            return base.IndexOf(item, index, count);
        }

        /// <summary>
        /// Find index of given item with a start index.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public int IndexOf(T item, int index)
        {
            return base.IndexOf(item, index);
        }
        
        /// <summary>
        /// Find index of given item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(T item)
        {
            return base.IndexOf(item);
        }

        /// <summary>
        /// Unsupported.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, T item)
        {
            throw this._readonly;
        }
        
        /// <summary>
        /// Unsupported.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="collection"></param>
        public void InsertRange(int index, IEnumerable<T> collection)
        {
            throw this._readonly;
        }
        
        /// <summary>
        /// Find last index of item.
        /// </summary>
        /// <param name="item">Item to find</param>
        /// <returns>Index</returns>
        public int LastIndexOf(T item)
        {
            return base.LastIndexOf(item);
        }
        
        /// <summary>
        /// Find last index of item
        /// </summary>
        /// <param name="item">Item to find</param>
        /// <param name="index">Index to start</param>
        /// <returns>Index found</returns>
        public int LastIndexOf(T item, int index)
        {
            return base.LastIndexOf(item, index);
        }
        
        /// <summary>
        /// Find last index of item
        /// </summary>
        /// <param name="item">item</param>
        /// <param name="index">start index</param>
        /// <param name="count">how many to search from start</param>
        /// <returns></returns>
        public int LastIndexOf(T item, int index, int count)
        {
            return base.LastIndexOf(item, index);
        }
        
        /// <summary>
        /// Unsupported.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            throw _readonly;
        }
        
        /// <summary>
        /// Unsupported.
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public int RemoveAll(Predicate<T> match)
        {
            throw this._readonly;
        }
        
        /// <summary>
        /// Unsupported.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            throw this._readonly;
        }
        
        /// <summary>
        /// Unsupported.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        public void RemoveRange(int index, int count)
        {
            throw this._readonly;
        }

        /// <summary>
        /// Unsupported.
        /// </summary>
        public void Reverse(int index, int count)
        {
            throw this._readonly;
        }
        
        /// <summary>
        /// Unsupported.
        /// </summary>
        public void Reverse()
        {
            throw this._readonly;
        }
        
        /// <summary>
        /// Unsupported.
        /// </summary>
        /// <param name="comparison"></param>
        public void Sort(Comparison<T> comparison)
        {
            throw this._readonly;
        }
        
        /// <summary>
        /// Unsupported.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <param name="comparer"></param>
        public void Sort(int index, int count, IComparer<T> comparer)
        {
            throw this._readonly;
        }
        
        /// <summary>
        /// Unsupported.
        /// </summary>
        public void Sort()
        {
            throw this._readonly;
        }
        
        /// <summary>
        /// Unsupported.
        /// </summary>
        /// <param name="comparer"></param>
        public void Sort(IComparer<T> comparer)
        {
            throw this._readonly;
        }
        
        /// <summary>
        /// Make an array of this.
        /// </summary>
        /// <returns></returns>
        public T[] ToArray()
        {
            return base.ToArray();
        }

        /// <summary>
        /// Unsupported.
        /// </summary>
        public void TrimExcess()
        {
            throw this._readonly;
        }

        /// <summary>
        /// Is the predicate true for all elements
        /// </summary>
        /// <param name="match"></param>
        /// <returns>true if all match</returns>
        public bool TrueForAll(Predicate<T> match)
        {
            return base.TrueForAll(match);
        }
    }
}