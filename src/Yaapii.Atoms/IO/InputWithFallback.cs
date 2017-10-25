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
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.IO
{
    /// <summary>
    /// Input which returns an alternate value if it fails.
    /// </summary>
    public sealed class InputWithFallback : IInput, IDisposable
    {
        /// <summary>
        /// main input
        /// </summary>
        private readonly IInput _main;

        /// <summary>
        /// alternative input
        /// </summary>
        private readonly IFunc<IOException, IInput> _alternative;

        /// <summary>
        /// Input which returns an alternate value if it fails.
        /// </summary>
        /// <param name="input">the input</param>
        public InputWithFallback(IInput input) : this(input, new DeadInput())
        { }

        /// <summary>
        /// Input which returns an alternate value if it fails.
        /// </summary>
        /// <param name="input">the input</param>
        /// <param name="alt">a fallback input</param>
        public InputWithFallback(IInput input, IInput alt) : this(input, (error) => alt)
        { }

        ///// <summary>
        ///// Input which returns an alternate value from the given <see cref="IFunc{In, Out}"/> if it fails.
        ///// </summary>
        ///// <param name="input">the input</param>
        ///// <param name="alt">a fallback input</param>
        //public InputWithFallback(IInput input, IFunc<IOException, IInput> alt) : this(
        //    input, new IoCheckedFunc<IOException, IInput>(alt))
        //{ }

        /// <summary>
        /// Input which returns an alternate value from the given <see cref="Func{IOException, IInput}"/>if it fails.
        /// </summary>
        /// <param name="input">the input</param>
        /// <param name="alt">function to return alternative input</param>
        public InputWithFallback(IInput input, Func<IOException, IInput> alt) : this(input,
            new FuncOf<IOException, IInput>(alt))
        { }

        /// <summary>
        /// Input which returns an alternate value from the given <see cref="IFunc{IOException, IInput}"/>if it fails.
        /// </summary>
        /// <param name="input">the input</param>
        /// <param name="alt">an alternative input</param>
        public InputWithFallback(IInput input, IFunc<IOException, IInput> alt)
        {
            this._main = input;
            this._alternative = alt;
        }

        /// <summary>
        /// Get the stream.
        /// </summary>
        /// <returns>the stream</returns>
        public Stream Stream()
        {
            Stream stream;
            try
            {
                stream = this._main.Stream();
            }
            catch (IOException ex)
            {
                stream = this._alternative.Invoke(ex).Stream();
            }
            return stream;
        }

        /// <summary>
        /// Clean up.
        /// </summary>
        public void Dispose()
        {
            ((IDisposable)this.Stream()).Dispose();
        }

    }
}
