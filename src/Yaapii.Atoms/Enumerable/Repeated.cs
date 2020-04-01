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

using System.Collections.Generic;
using Yaapii.Atoms.Scalar;

#pragma warning disable NoGetOrSet // No Statics
#pragma warning disable CS1591
namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// <see cref="IEnumerable{T}"/> which repeats one element multiple times.
    /// </summary>
    /// <typeparam name="T">type of element to repeat</typeparam>
    public sealed class Repeated<T> : ManyEnvelope<T>
    {
        /// <summary>
        /// <see cref="IEnumerable{T}"/> which repeats one element multiple times.
        /// </summary>
        /// <param name="elm">function to get element to repeat</param>
        /// <param name="cnt">how often to repeat</param>
        public Repeated(System.Func<T> elm, int cnt) :
            this(new LiveScalar<T>(elm), cnt)
        { }

        /// <summary>
        /// <see cref="IEnumerable{T}"/> which repeats one element multiple times.
        /// </summary>
        /// <param name="elm">element to repeat</param>
        /// <param name="cnt">how often to repeat</param>
        public Repeated(T elm, int cnt) :
            this(new LiveScalar<T>(elm), cnt)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="elm">scalar to get element to repeat</param>
        /// <param name="cnt">how often to repeat</param>
        public Repeated(IScalar<T> elm, int cnt) : this(
            elm, new LiveScalar<int>(cnt))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="elm">scalar to get element to repeat</param>
        /// <param name="cnt">how often to repeat</param>
        public Repeated(IScalar<T> elm, IScalar<int> cnt) : base(() => 
            new LiveMany<T>(() =>
                new Enumerator.Repeated<T>(elm, cnt.Value())
            ),
            false
        )
        { }
    }
}
#pragma warning restore NoGetOrSet // No Statics