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
using System.Collections.Generic;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Collection
{
    /// <summary>
    /// Ensures that <see cref="ICollection{T}" /> is not empty/>
    /// </summary>
    /// <typeparam name="T">Type of the collection</typeparam>
    public sealed class NotEmpty<T> : Collection.Envelope<T>
    {
        /// <summary>
        /// Ensures that <see cref="ICollection{T}" /> is not empty/>
        /// </summary>
        /// <param name="origin">Collection</param>
        public NotEmpty(ICollection<T> origin) : this(
            origin,
            new Exception("Collection is empty"))
        { }

        /// <summary>
        /// Ensures that <see cref="ICollection{T}" /> is not empty/>
        /// </summary>
        /// <param name="origin">Collection</param>
        /// <param name="ex">Execption to be thrown if empty</param>
        public NotEmpty(ICollection<T> origin, Exception ex) : base(
            new ScalarOf<ICollection<T>>(
                () =>
                {
                    new FailPrecise(
                        new FailEmpty<T>(
                            origin),
                        ex).Go();
                    return origin;
                }
            ),
            false
        )
        { }
    }
}
