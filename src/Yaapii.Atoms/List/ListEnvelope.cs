// MIT License
//
// Copyright(c) 2019 ICARUS Consulting GmbH
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
    public abstract class ListEnvelope<T> : IList<T>
    {
        private readonly IScalar<IList<T>> lst;
        private readonly UnsupportedOperationException readOnlyError = new UnsupportedOperationException("The list is readonly.");

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fnc">Function delivering a <see cref="IList{T}"/></param>
        public ListEnvelope(Func<IList<T>> fnc) : this(new ScalarOf<IList<T>>(fnc))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="sc"></param>
        public ListEnvelope(IScalar<IList<T>> sc)
        {
            lst = sc;
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
                return this.lst.Value()[index];
            }
            set
            {
                throw this.readOnlyError;
            }
        }

        /// <summary>
        /// Count elements
        /// </summary>
        public int Count { get { return this.lst.Value().Count; } }

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
        /// Test if containing the given item
        /// </summary>
        /// <param name="item">Item to find</param>
        /// <returns>true if item is found</returns>
        public bool Contains(T item)
        {
            return this.lst.Value().Contains(item);
        }

        /// <summary>
        /// copy to a target array
        /// </summary>
        /// <param name="array">target array</param>
        /// <param name="arrayIndex">write start index</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            this.lst.Value().CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Enumerator for this list.
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.lst.Value().GetEnumerator();
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
            return this.lst.Value().IndexOf(item);
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