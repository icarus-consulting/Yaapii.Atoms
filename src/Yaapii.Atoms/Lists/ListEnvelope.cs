// MIT License
//
// Copyright(c) 2020 ICARUS Consulting GmbH
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Collections;
using System.Collections.Generic;
using Yaapii.Atoms.Enumerator;
using Yaapii.Atoms.Fail;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword

namespace Yaapii.Atoms.List
{
    /// <summary>
    /// List envelope. Can make a readonly list from a scalar.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ListEnvelope<T> : IList<T>
    {
        private readonly UnsupportedOperationException readOnlyError = new UnsupportedOperationException("The list is readonly.");
        private readonly Func<IList<T>> origin;
        private readonly Enumerator.Cached<T>.Cache<T> enumeratorCache;
        private readonly bool live;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="lst">A scalar to a <see cref="IList{T}"/></param>
        /// <param name="live">value is handled live or sticky</param>
        public ListEnvelope(IScalar<IList<T>> lst, bool live) : this(() => lst.Value(), live)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="lst">List generator</param>
        /// <param name="live">value is handled live or sticky</param>
        public ListEnvelope(Func<IList<T>> lst, bool live)
        {
            this.origin = lst;
            this.live = live;
            this.enumeratorCache = new Enumerator.Cached<T>.Cache<T>(() => lst().GetEnumerator());
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
                T result;
                if (this.live)
                {
                    result = this.origin()[index];
                }
                else
                {
                    if (this.enumeratorCache.ContainsKey(index))
                    {
                        result = this.enumeratorCache[index];
                    }
                    else
                    {
                        throw new ArgumentException($"Cannot get item at index {index} from list because it has only {this.enumeratorCache.Count} items.");
                    }
                }
                return result;
            }
            set
            {
                throw this.readOnlyError;
            }
        }

        /// <summary>
        /// Count elements
        /// </summary>
        public int Count
        {
            get
            {
                var count = 0;
                if (this.live)
                {
                    count = this.origin().Count;
                }
                else
                {
                    count = this.enumeratorCache.Count;
                }
                return count;
            }
        }

        /// <summary>
        /// Test if containing the given item
        /// </summary>
        /// <param name="item">Item to find</param>
        /// <returns>true if item is found</returns>
        public bool Contains(T item)
        {
            bool result = false;
            if (this.live)
            {
                result = this.origin().Contains(item);
            }
            else
            {
                var idx = -1;
                while (this.enumeratorCache.ContainsKey(idx))
                {
                    if (this.enumeratorCache[idx].Equals(item))
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// copy to a target array
        /// </summary>
        /// <param name="array">target array</param>
        /// <param name="arrayIndex">write start index</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            int idx = 0;
            if (this.live)
            {
                this.origin().CopyTo(array, arrayIndex);
            }
            else
            {
                while (this.enumeratorCache.ContainsKey(idx))
                {
                    array[arrayIndex + idx] = this.enumeratorCache[idx];
                    idx++;
                }
            }
        }

        /// <summary>
        /// Enumerator for this list.
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            IEnumerator<T> result;
            if (this.live)
            {
                result = this.origin().GetEnumerator();
            }
            else
            {
                result = new Cached<T>(this.enumeratorCache);
            }
            return result;
        }

        /// <summary>
        /// Enumerator for this list.
        /// </summary>
        /// <returns>Enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Index of given item
        /// </summary>
        /// <param name="item">item</param>
        /// <returns></returns>
        public int IndexOf(T item)
        {
            var result = -1;
            if (this.live)
            {
                result = this.origin().IndexOf(item);
            }
            else
            {
                var pos = 0;
                while (this.enumeratorCache.ContainsKey(pos))
                {
                    if (this.enumeratorCache[pos].Equals(item))
                    {
                        result = pos;
                        break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// This is a readonly collection, always true.
        /// </summary>
        public bool IsReadOnly => true;

        /// <summary>
        /// Not supported.
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item) { throw this.readOnlyError; }

        /// <summary>
        /// Unsupported.
        /// </summary>
        public void Clear()
        {
            throw this.readOnlyError;
        }

        /// <summary>
        /// Unsupported.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, T item)
        {
            throw this.readOnlyError;
        }

        /// <summary>
        /// Unsupported.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            throw readOnlyError;
        }

        /// <summary>
        /// Unsupported.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            throw this.readOnlyError;
        }
    }
}