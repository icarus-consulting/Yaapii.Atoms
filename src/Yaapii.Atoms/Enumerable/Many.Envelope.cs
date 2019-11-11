﻿// MIT License
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

namespace Yaapii.Atoms.Enumerable
{
    public partial class Many
    {
        /// <summary>
        /// Envelope for Enumerable of strings.
        /// It bundles the methods offered by IEnumerable and enables scalar based ctors.
        /// </summary>
        public abstract class Envelope : IEnumerable<string>
        {
            private readonly bool live;
            private readonly Func<IEnumerator<string>> originLive;
            private readonly Lazy<IEnumerator<string>> origin;

            /// <summary>
            /// Envelope for Enumerable.
            /// </summary>
            public Envelope(IScalar<IEnumerable<string>> fnc) : this(() => fnc.Value().GetEnumerator())
            { }

            /// <summary>
            /// Envelope for Enumerable.
            /// </summary>
            public Envelope(Func<IEnumerable<string>> origin) : this(() => origin().GetEnumerator())
            { }

            /// <summary>
            /// Envelope for Enumerable.
            /// </summary>
            public Envelope(Func<IEnumerator<string>> origin, bool live = false)
            {
                this.live = live;
                this.originLive = origin;
                this.origin = new Lazy<IEnumerator<string>>(() => origin());
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
                    result = this.originLive();
                }
                else
                {
                    result = this.origin.Value;
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
        public abstract class Envelope<T> : IEnumerable<T>
        {
            private readonly bool live;
            private readonly Lazy<IEnumerable<T>> origin;
            private readonly Func<IEnumerable<T>> originLive;

            /// <summary>
            /// Envelope for Enumerable.
            /// </summary>
            public Envelope(IScalar<IEnumerable<T>> fnc, bool live = false) : this(() => fnc.Value(), live)
            { }

            /// <summary>
            /// Envelope for Enumerable.
            /// </summary>
            public Envelope(Func<IEnumerator<T>> origin, bool live = false) : this(
                () =>
                {
                    var lst = new List<T>();
                    var enm = origin();
                    while(enm.MoveNext())
                    {
                        lst.Add(enm.Current);
                    }
                    return lst;
                }
            )
            { }

            /// <summary>
            /// Envelope for Enumerable
            /// </summary>
            /// <param name="origin">How to get the enumerator</param>
            /// <param name="live">Should the object build the enumerator live, every time it is used?</param>
            public Envelope(Func<IEnumerable<T>> origin, bool live = false)
            {
                this.live = live;
                this.origin = new Lazy<IEnumerable<T>>(origin);
                this.originLive = origin;
            }

            /// <summary>
            /// Enumerator for this envelope.
            /// </summary>
            /// <returns>The enumerator</returns>
            public IEnumerator<T> GetEnumerator()
            {
                return this.origin.Value.GetEnumerator();
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
}
