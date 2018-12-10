﻿// MIT License
//
// Copyright(c) 2017 ICARUS Consulting GmbH
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
using Yaapii.Atoms.Fail;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Collection
{
    /// <summary>
    /// Envelope for Collections. It enables ICollection classes frmo .Net to accept scalars.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CollectionEnvelope<T> : ICollection<T>
    {
        /// <summary>
        /// scalar of collection
        /// </summary>
        private readonly IScalar<ICollection<T>> _col;
        private readonly UnsupportedOperationException _readonlyError = new UnsupportedOperationException("The collection is readonly");

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fnc">Func which delivers <see cref="ICollection{T}"/></param>
        public CollectionEnvelope(Func<ICollection<T>> fnc) : this(new ScalarOf<ICollection<T>>(fnc))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="slr">Scalar of ICollection</param>
        public CollectionEnvelope(IScalar<ICollection<T>> slr)
        {
            this._col = slr;
        }

        /// <summary>
        /// Number of elements
        /// </summary>
        public int Count => this._col.Value().Count;

        /// <summary>
        /// Is the collection readonly?
        /// </summary>
        public bool IsReadOnly => true;

        /// <summary>
        /// Add an element
        /// </summary>
        /// <param name="item">Item to add</param>
        public void Add(T item)
        {
            throw this._readonlyError;
        }

        /// <summary>
        /// Clear all items
        /// </summary>
        public void Clear()
        {
            throw this._readonlyError;
        }

        /// <summary>
        /// Test if the collection contains an item
        /// </summary>
        /// <param name="item">Item to lookup</param>
        /// <returns>True if item is found</returns>
        public bool Contains(T item)
        {
            return this._col.Value().Contains(item);
        }

        /// <summary>
        /// Copies items from given index to target array
        /// </summary>
        /// <param name="array">Target array</param>
        /// <param name="arrayIndex">Index to start</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            this._col.Value().CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// A enumerator to iterate through the items.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this._col.Value().GetEnumerator();
        }

        /// <summary>
        /// Remove an item
        /// </summary>
        /// <param name="item">Item to remove</param>
        /// <returns>True if success</returns>
        public bool Remove(T item)
        {
            throw this._readonlyError;
        }

        /// <summary>
        /// Get the enumerator to iterate through the items
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._col.Value().GetEnumerator();
        }
    }
}
