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
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Does to all elements in a <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <typeparam name="In"></typeparam>
    public sealed class Each<In> : IAction
    {
        private readonly IAction<In> act;
        private readonly IEnumerable<In> enumerable;

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
            proc, new ManyOf<In>(src))
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
            act = action;
            this.enumerable = enumerable;
        }

        /// <summary>
        /// Execute Action for each element
        /// </summary>
        public void Invoke()
        {
            foreach (var item in enumerable)
            {
                act.Invoke(item);
            }
        }
    }

    public static class Each
    {
        /// <summary>
        /// Executes the given Action for every element in the params.
        /// Replaces ForEach in LinQ. 
        /// <para>Object is <see cref="IAction"/></para>
        /// </summary>
        /// <param name="act">the condition to apply</param>
        /// <param name="src">list of items</param>
        public static IAction New<In>(Action<In> act, params In[] src)
            => new Each<In>(act, src);

        /// <summary>
        ///  Executes the given Action for every element in the Enumerable.
        /// Replaces ForEach in LinQ.
        /// <para>Object is <see cref="IAction"/></para>
        /// </summary>
        /// <param name="act">the condition to apply</param>
        /// <param name="src">list of items</param>
        public static IAction New<In>(Action<In> act, IEnumerable<In> src)
            => new Each<In>(act, src);

        /// <summary>
        ///  Executes the given IAction for every element in the params.
        /// Replaces ForEach in LinQ.
        /// <para>Object is <see cref="IAction"/></para>        /// </summary>
        /// <param name="act">the condition to apply</param>
        /// <param name="src">list of items</param>
        public static IAction New<In>(IAction<In> act, params In[] src)
            => new Each<In>(act, src);


        /// <summary>
        ///  Executes the given IAction for every element in the Enumerable.
        /// Replaces ForEach in LinQ.
        /// <para>Object is <see cref="IAction"/></para>
        /// </summary>
        /// <param name="action"></param>
        /// <param name="enumerable"></param>
        public static IAction New<In>(IAction<In> action, IEnumerable<In> enumerable)
            => new Each<In>(action, enumerable);
    }
}
