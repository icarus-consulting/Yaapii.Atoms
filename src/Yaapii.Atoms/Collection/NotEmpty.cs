// MIT License
//
// Copyright(c) 2023 ICARUS Consulting GmbH
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
    public sealed class NotEmpty<T> : CollectionEnvelope<T>
    {
        /// <summary>
        /// Ensures that <see cref="ICollection{T}" /> is not empty/>
        /// </summary>
        /// <param name="origin">Collection</param>
        public NotEmpty(ICollection<T> origin) : this(
            origin,
            new Exception("Collection is empty"))
        { }
        public static NotEmpty<T> New(ICollection<T> origin) => new NotEmpty<T>(origin);

        /// <summary>
        /// Ensures that <see cref="ICollection{T}" /> is not empty/>
        /// </summary>
        /// <param name="origin">Collection</param>
        /// <param name="ex">Execption to be thrown if empty</param>
        public NotEmpty(ICollection<T> origin, Exception ex) : this(
            origin, () => throw ex
        )
        { }

        /// <summary>
        /// Ensures that <see cref="ICollection{T}" /> is not empty/>
        /// </summary>
        /// <param name="origin">Collection</param>
        /// <param name="ex">Execption to be thrown if empty</param>
        public NotEmpty(ICollection<T> origin, Func<ICollection<T>> fallback) : base(
            new Live<ICollection<T>>(
                () =>
                {
                    if (!origin.GetEnumerator().MoveNext())
                    {
                        origin = fallback();
                    }
                    return origin;
                }
            ),
            false
        )
        { }
        public static NotEmpty<T> New(ICollection<T> origin, Func<ICollection<T>> fallback) => new NotEmpty<T>(origin, fallback);
    }

    /// <summary>
    /// Ensures that <see cref="ICollection{T}" /> is not empty/>
    /// </summary>
    public static class NotEmpty
    {
        public static ICollection<T> New<T>(ICollection<T> origin) => new NotEmpty<T>(origin);

        public static ICollection<T> New<T>(ICollection<T> origin, Func<ICollection<T>> fallback) => new NotEmpty<T>(origin, fallback);
    }
}
