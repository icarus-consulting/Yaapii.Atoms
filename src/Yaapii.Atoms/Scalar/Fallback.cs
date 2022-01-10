// MIT License
//
// Copyright(c) 2022 ICARUS Consulting GmbH
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
    /// Scalar which calls a fallback function if Value() fails.
    /// </summary>
    /// <typeparam name="Out">Type of output value</typeparam>
    public class Fallback<Out> : ScalarEnvelope<Out>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="origin">Scalar which can fail</param>
        /// <param name="fallback">Fallback if scalar fails</param>
        public Fallback(IScalar<Out> origin, Out fallback) : this(
            origin,
            (ex) => fallback)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="origin">Scalar which can fail</param>
        /// <param name="fallback">Fallback if scalar fails</param>
        public Fallback(IScalar<Out> origin, Func<Out> fallback) : this(
            origin,
            (ex) => fallback.Invoke())
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="origin">scalar which can fail</param>
        /// <param name="fallback">fallback to apply when fails</param>
        public Fallback(IScalar<Out> origin, Func<Exception, Out> fallback)
            : base(() =>
            {
                try
                {
                    return origin.Value();
                }
                catch (Exception ex)
                {
                    return fallback.Invoke(ex);
                }
            })
        { }
    }

    public static class Fallback
    {

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="origin">Scalar which can fail</param>
        /// <param name="fallback">Fallback if scalar fails</param>
        public static Fallback<Out> New<Out>(IScalar<Out> origin, Out fallback)
            => new Fallback<Out>(origin, fallback);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="origin">Scalar which can fail</param>
        /// <param name="fallback">Fallback if scalar fails</param>
        public static Fallback<Out> New<Out>(IScalar<Out> origin, Func<Out> fallback)
            => new Fallback<Out>(origin, fallback);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="origin">scalar which can fail</param>
        /// <param name="fallback">fallback to apply when fails</param>
        public static Fallback<Out> New<Out>(IScalar<Out> origin, Func<Exception, Out> fallback)
            => new Fallback<Out>(origin, fallback);
    }
}
