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
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Error
{
    /// <summary>
    /// Fail always with the given message.
    /// </summary>
    public sealed class FailAlways : IFail
    {
        private readonly Func<Exception> error;

        /// <summary>
        /// Fail always with <see cref="System.Exception"/> with the given message.
        /// </summary>
        /// <param name="msg">message to wrap in exception</param>
        public FailAlways(string msg) : this(new TextOf(msg))
        { }

        /// <summary>
        /// Fail always with <see cref="System.Exception"/> with the given message.
        /// </summary>
        /// <param name="msg">message to wrap in exception</param>
        public FailAlways(IText msg) : this(() => new Exception(msg.AsString()))
        { }

        /// <summary>
        /// Fail always with specified exception with the given message.
        /// </summary>
        /// <param name="error">ex to throw</param>
        private FailAlways(Exception error) : this(() => error)
        { }

        /// <summary>
        /// Fail always with specified exception with the given message.
        /// </summary>
        /// <param name="error">ex to throw</param>
        private FailAlways(Func<Exception> error)
        {
            this.error = error;
        }

        /// <summary>
        /// Fail if necessary.
        /// </summary>
        public void Go()
        {
            throw error();
        }
    }
}
