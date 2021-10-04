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
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// Envelope for Enumerable of strings.
    /// It bundles the methods offered by IEnumerable and enables scalar based ctors.
    /// </summary>
    public abstract class ManyEnvelope : IEnumerable<string>
    {
        private readonly bool live;
        private readonly Func<IEnumerator<string>> origin;
        private readonly ScalarOf<IList<string>> fixedList;

        /// <summary>
        /// Envelope for Enumerable.
        /// </summary>
        public ManyEnvelope(IScalar<IEnumerable<string>> fnc) : this(
            fnc,
            false
        )
        { }

        /// <summary>
        /// Envelope for Enumerable.
        /// </summary>
        public ManyEnvelope(Func<IEnumerator<string>> origin) : this(
            origin,
            false
        )
        { }

        /// <summary>
        /// Envelope for Enumerable.
        /// </summary>
        public ManyEnvelope(IScalar<IEnumerable<string>> fnc, bool live) : this(() =>
            fnc.Value(),
            live
        )
        { }

        /// <summary>
        /// Envelope for Enumerable.
        /// </summary>
        public ManyEnvelope(Func<IEnumerable<string>> origin, bool live = false) : this(() =>
            origin().GetEnumerator(),
            live
        )
        { }

        /// <summary>
        /// Envelope for Enumerable.
        /// </summary>
        public ManyEnvelope(Func<IEnumerator<string>> origin, bool live)
        {
            this.live = live;
            this.origin = origin;
            this.fixedList =
                new ScalarOf<IList<string>>(() =>
                {
                    var result = new List<string>();
                    var enm = origin();
                    while (enm.MoveNext())
                    {
                        result.Add(enm.Current);
                    }
                    return result;
                });
        }

        /// <summary>
        /// Enumerator for this envelope.
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<string> GetEnumerator()
        {
            IEnumerator<string> result;
            if (this.live)
            {
                result = this.origin();
            }
            else
            {
                result = this.fixedList.Value().GetEnumerator();
            }
            return result;
        }

        /// <summary>
        /// Enumerator for this envelope.
        /// </summary>
        /// <returns>The enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// Envelope for Enumerable.
    /// It bundles the methods offered by IEnumerable and enables scalar based ctors.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ManyEnvelope<T> : IEnumerable<T>
    {
        private readonly bool live;
        private readonly Enumerator.Sticky<T>.Cache<T> enumeratorCache;
        private readonly Func<IEnumerator<T>> origin;

        /// <summary>
        /// Envelope for Enumerable.
        /// </summary>
        public ManyEnvelope(IScalar<IEnumerable<T>> fnc, bool live) : this(() => fnc.Value(), live)
        { }

        /// <summary>
        /// Envelope for Enumerable.
        /// </summary>
        public ManyEnvelope(Func<IEnumerable<T>> origin, bool live) : this(() => origin().GetEnumerator(), live)
        { }

        /// <summary>
        /// Envelope for Enumerables.
        /// </summary>
        public ManyEnvelope(Func<IEnumerator<T>> origin, bool live)
        {
            this.live = live;
            this.enumeratorCache = new Sticky<T>.Cache<T>(origin);
            this.origin = origin;
        }

        /// <summary>
        /// Enumerator for this envelope.
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            IEnumerator<T> result;
            if (this.live)
            {
                result = this.origin();
            }
            else
            {
                result = new Enumerator.Sticky<T>(this.enumeratorCache);
            }
            return result;
        }

        /// <summary>
        /// Enumerator for this envelope.
        /// </summary>
        /// <returns>The enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
