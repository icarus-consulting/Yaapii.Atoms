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

using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// Scalar which calls a fallback function if Value() fails.
    /// </summary>
    /// <typeparam name="Out">Type of output value</typeparam>
    public class Fallback<Out> : IScalar<Out>
    {
        private readonly IScalar<Out> origin;
        private readonly Func<Exception, Out> fbk;

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
        /// <param name="fbk">fallback to apply when fails</param>
        public Fallback(IScalar<Out> origin, Func<Exception, Out> fbk)
        {
            this.origin = origin;
            this.fbk = fbk;
        }

        /// <summary>
        /// Get the value or fallback if fails
        /// </summary>
        /// <returns>value or fallback value</returns>
        public Out Value()
        {
            try
            {
                return origin.Value();
            }
            catch (Exception ex)
            {
                return fbk.Invoke(ex);
            }

        }
    }
}
