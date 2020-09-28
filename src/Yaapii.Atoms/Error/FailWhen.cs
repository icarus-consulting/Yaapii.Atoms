// MIT License
//
// Copyright(c) 2020 ICARUS Consulting GmbH
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
    /// Fail if condition is matched.
    /// </summary>
    public sealed class FailWhen : IFail
    {
        private readonly Func<bool> _condition;
        private readonly Exception _ex;

        /// <summary>
        /// Fail with <see cref="System.ArgumentException"/> if condition is matched.
        /// </summary>
        /// <param name="condition">condition to apply</param>
        public FailWhen(bool condition) : this(condition, "Failed because the given function failed.")
        { }

        /// <summary>
        /// Fail with <see cref="System.ArgumentException"/> if condition is matched.
        /// </summary>
        /// <param name="condition">condition to apply</param>
        /// <param name="hint">msg to put in exception</param>
        public FailWhen(bool condition, string hint) : this(() => condition, hint)
        { }

        /// <summary>
        /// Fail with <see cref="System.ArgumentException"/> if condition is matched.
        /// </summary>
        /// <param name="condition">condition to apply</param>
        /// <param name="ex">specific exception which will be thrown</param>
        public FailWhen(bool condition, Exception ex) : this(() => condition, ex)
        { }

        /// <summary>
        /// Fail if condition is matched.
        /// </summary>
        /// <param name="condition">condition to apply</param>
        public FailWhen(Func<bool> condition) : this(condition, "Failed because the given function failed.")
        { }

        /// <summary>
        /// Fail with <see cref="System.ArgumentException"/> if condition is matched.
        /// </summary>
        /// <param name="condition">condition to apply</param>
        /// <param name="hint">msg to put in exception</param>
        public FailWhen(Func<bool> condition, string hint) : this(
            condition, new ArgumentException(hint)
        )
        { }

        /// <summary>
        /// Fail with specified exception if condition is matched.
        /// </summary>
        /// <param name="condition">condition to apply</param>
        /// <param name="ex">specific exception which will be thrown</param>
        public FailWhen(Func<bool> condition, Exception ex)
        {
            this._condition = condition;
            this._ex = ex;
        }

        /// <summary>
        /// Fail if necessary.
        /// </summary>
        public void Go()
        {
            if (_condition.Invoke()) throw this._ex;
        }
    }
}
