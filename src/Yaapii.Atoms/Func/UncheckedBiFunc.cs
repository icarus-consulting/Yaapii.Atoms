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
using System.IO;
using System.Text;
using Yaapii.Atoms.Error;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// BiFunc that does not throw <see cref="IOException" /> but throws <see cref="UncheckedIOException"/> instead.
    /// </summary>
    /// <typeparam name="In1">type of first argument</typeparam>
    /// <typeparam name="In2">type of second argument</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class UncheckedBiFunc<In1, In2, Out> : IBiFunc<In1, In2, Out>
    {
        /// <summary>
        /// original func
        /// </summary>
        private readonly IBiFunc<In1, In2, Out> func;

        /// <summary>
        /// BiFunc that does not throw <see cref="IOException" /> but throws <see cref="UncheckedIOException"/> instead.
        /// </summary>
        /// <param name="fnc">func to call</param>
        public UncheckedBiFunc(System.Func<In1, In2, Out> fnc) : this(new BiFuncOf<In1, In2, Out>(fnc))
        { }

        /// <summary>
        /// BiFunc that does not throw <see cref="IOException" /> but throws <see cref="UncheckedIOException"/> instead.
        /// </summary>
        /// <param name="fnc">func to call</param>
        public UncheckedBiFunc(IBiFunc<In1, In2, Out> fnc)
        {
            this.func = fnc;
        }

        /// <summary>
        /// Invoke function with arguments and retrieve output.
        /// </summary>
        /// <param name="first">first argument</param>
        /// <param name="second">second argument</param>
        /// <returns></returns>
        public Out Apply(In1 first, In2 second)
        {
            try
            {
                return new IoCheckedBiFunc<In1, In2, Out>(this.func).Apply(first, second);
            }
            catch (IOException ex)
            {
                throw new UncheckedIOException(ex);
            }
        }
    }
}
