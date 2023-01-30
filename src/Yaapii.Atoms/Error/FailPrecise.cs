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

namespace Yaapii.Atoms.Error
{
    /// <summary>
    /// Fail with changed exception.
    /// </summary>
    public sealed class FailPrecise : IFail
    {
        private readonly IFail _origin;
        private readonly Exception _precision;

        /// <summary>
        /// Decorates a Failing by replacing the thrown Exception with the injected one
        /// </summary>
        /// <param name="origin">The Failing to decorate</param>
        /// <param name="precision">The new Exception to throw when necessary</param>
        public FailPrecise(IFail origin, Exception precision)
        {
            _origin = origin;
            _precision = precision;
        }

        /// <summary>
        /// Fail if necessary.
        /// </summary>
        public void Go()
        {
            try
            {
                _origin.Go();
            }
            catch (Exception)
            {
                throw _precision;
            }
        }
    }
}
