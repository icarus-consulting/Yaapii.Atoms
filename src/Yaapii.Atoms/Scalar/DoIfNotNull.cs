// MIT License
//
// Copyright(c) 2026 ICARUS Consulting GmbH
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

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// Executes a function if the input is not null, otherwise returns a fallback value.
    /// </summary>
    public sealed class DoIfNotNull<In, Result> : ScalarEnvelope<Result>
    {
        public DoIfNotNull(In input, Result fallback, Func<In, Result> continued) : this(
            () => input,
            fallback,
            continued
        )
        { }

        /// <summary>
        /// Executes a function if the input is not null, otherwise returns a fallback value.
        /// </summary>
        public DoIfNotNull(Func<In> input, Result fallback, Func<In, Result> continued) : base(() =>
        {
            var result = fallback;

            var prep = input.Invoke();
            if (prep != null)
            {
                result = continued.Invoke(prep);
            }

            return result;
        })
        { }
    }

    public static class DoIfNotNull
    {
        /// <summary>
        /// Executes a function if the input is not null, otherwise returns a fallback value.
        /// </summary>
        public static IScalar<Result> New<In, Result>(In input, Result fallback, Func<In, Result> continued) =>
            new DoIfNotNull<In, Result>(input, fallback, continued);

        /// <summary>
        /// Executes a function if the input is not null, otherwise returns a fallback value.
        /// </summary>
        public static IScalar<Result> New<In, Result>(Func<In> input, Result fallback, Func<In, Result> continued) =>
            new DoIfNotNull<In, Result>(input, fallback, continued);
    }
}
