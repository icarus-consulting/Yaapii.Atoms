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

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Function that repeats its calculation a few times before returning the result.
    /// </summary>
    /// <typeparam name="In">type of input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class RepeatedFunc<In, Out> : IFunc<In, Out>
    {
        /// <summary>
        /// function to call
        /// </summary>
        private readonly IFunc<In, Out> func;

        /// <summary>
        /// how often to run
        /// </summary>
        private readonly int times;

        /// <summary>
        /// Function that repeats its calculation a few times before returning the result.
        /// </summary>
        /// <param name="fnc">function to call</param>
        /// <param name="max">how often it repeats</param>
        public RepeatedFunc(Func<In, Out> fnc, int max) : this(new FuncOf<In, Out>((X) => fnc(X)), max)
        { }

        /// <summary>
        /// Function that repeats its calculation a few times before returning the result.
        /// </summary>
        /// <param name="fnc">function to call</param>
        /// <param name="max">how often it repeats</param>
        public RepeatedFunc(IFunc<In, Out> fnc, int max)
        {
            this.func = fnc;
            this.times = max;
        }

        /// <summary>
        /// Invoke the function and retrieve the output.
        /// </summary>
        /// <param name="input">the input argument</param>
        /// <returns>the output</returns>
        public Out Invoke(In input)
        {
            if (this.times <= 0)
            {
                throw new ArgumentException("The number of repetitions must be at least 1");
            }

            Out result;
            result = this.func.Invoke(input);
            for (int idx = 0; idx < this.times - 1; ++idx)
            {
                result = this.func.Invoke(input);
            }
            return result;
        }
    }

    public static class RepeatedFunc
    {
        /// <summary>
        /// Function that repeats its calculation a few times before returning the result.
        /// </summary>
        /// <param name="fnc">function to call</param>
        /// <param name="max">how often it repeats</param>
        public static IFunc<In, Out> New<In, Out>(Func<In, Out> fnc, int max) => new RepeatedFunc<In, Out>(fnc, max);

        /// <summary>
        /// Function that repeats its calculation a few times before returning the result.
        /// </summary>
        /// <param name="fnc">function to call</param>
        /// <param name="max">how often it repeats</param>
        public static IFunc<In, Out> New<In, Out>(IFunc<In, Out> fnc, int max) => new RepeatedFunc<In, Out>(fnc, max);
    }
}
