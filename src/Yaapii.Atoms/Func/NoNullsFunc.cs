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

using System.IO;
using Yaapii.Atoms.Error;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Function that does not allow null as input.
    /// </summary>
    /// <typeparam name="In">type of input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class NoNullsFunc<In, Out> : IFunc<In, Out>
    {
        /// <summary>
        /// The function
        /// </summary>
        private readonly IFunc<In, Out> _func;

        /// <summary>
        /// Function that does not allow null as input.
        /// </summary>
        /// <param name="func">the function</param>
        public NoNullsFunc(IFunc<In, Out> func)
        {
            _func = func;
        }

        /// <summary>
        /// Apply it
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>the output</returns>
        public Out Invoke(In input)
        {
            new FailNull(input, "got NULL instead of a valid function");

            Out result = _func.Invoke(input);
            if (result == null)
            {
                throw new IOException("got NULL instead of a valid result");
            }
            return result;
        }
    }
}
