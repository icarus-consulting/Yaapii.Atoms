// MIT License
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
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// Does to all elements in a <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <typeparam name="In"></typeparam>
    public sealed class Each<In> : IAction
    {
        private readonly IAction<In> _action;
        private readonly IEnumerable<In> _enumerable;

        /// <summary>
        /// Executes the given Action for every element in the params.
        /// Replaces ForEach in LinQ. 
        /// <para>Object is <see cref="IAction"/></para>
        /// </summary>
        /// <param name="proc">the condition to apply</param>
        /// <param name="src">list of items</param>
        public Each(Action<In> proc, params In[] src) : this(new ActionOf<In>(input => proc.Invoke(input)), src)
        { }

        /// <summary>
        ///  Executes the given Action for every element in the Enumerable.
        /// Replaces ForEach in LinQ.
        /// <para>Object is <see cref="IAction"/></para>
        /// </summary>
        /// <param name="proc">the condition to apply</param>
        /// <param name="src">list of items</param>
        public Each(Action<In> proc, IEnumerable<In> src) : this(new ActionOf<In>(ipt => proc.Invoke(ipt)), src)
        { }

        /// <summary>
        ///  Executes the given IAction for every element in the params.
        /// Replaces ForEach in LinQ.
        /// <para>Object is <see cref="IAction"/></para>        /// </summary>
        /// <param name="proc">the condition to apply</param>
        /// <param name="src">list of items</param>
        public Each(IAction<In> proc, params In[] src) : this(
            proc, new EnumerableOf<In>(src))
        { }


        /// <summary>
        ///  Executes the given IAction for every element in the Enumerable.
        /// Replaces ForEach in LinQ.
        /// <para>Object is <see cref="IAction"/></para>
        /// </summary>
        /// <param name="action"></param>
        /// <param name="enumerable"></param>
        public Each(IAction<In> action, IEnumerable<In> enumerable)
        {
            _action = action;
            _enumerable = enumerable;
        }

        /// <summary>
        /// Execute Action for each element
        /// </summary>
        public void Invoke()
        {
            foreach (var item in _enumerable)
            {
                _action.Invoke(item);
            }
        }
    }
}
