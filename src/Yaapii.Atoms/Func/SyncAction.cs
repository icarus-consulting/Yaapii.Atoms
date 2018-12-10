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
using System.Text;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Proc that is threadsafe.
    /// </summary>
    /// <typeparam name="In">type of input</typeparam>
    public sealed class SyncAction<In> : IAction<In>
    {
        /// <summary>
        /// original proc
        /// </summary>
        private readonly IAction<In> act;

        /// <summary>
        /// threadsafe-lock
        /// </summary>
        private readonly Object lck;

        /// <summary>
        /// Proc that is threadsafe.
        /// </summary>
        /// <param name="prc">proc to make threadsafe</param>
        public SyncAction(IAction<In> prc) : this(prc, prc)
        { }

        /// <summary>
        /// Proc that is threadsafe.
        /// </summary>
        /// <param name="prc">proc to make threadsafe</param>
        /// <param name="lck">object to lock threadsafe</param>
        public SyncAction(IAction<In> prc, object lck)
        {
            this.act = prc;
            this.lck = lck;
        }

        /// <summary>
        /// Execute procedure with given input.
        /// </summary>
        /// <param name="input"></param>
        public void Invoke(In input)
        {
            lock (this.lck)
            {
                this.act.Invoke(input);
            }
        }

    }

    /// <summary>
    /// Action that is threadsafe.
    /// </summary>
    public sealed class SyncAction : IAction
    {
        /// <summary>
        /// original proc
        /// </summary>
        private readonly IAction proc;

        /// <summary>
        /// threadsafe-lock
        /// </summary>
        private readonly Object lck;

        /// <summary>
        /// Proc that is threadsafe.
        /// </summary>
        /// <param name="prc">proc to make threadsafe</param>
        public SyncAction(IAction prc) : this(prc, prc)
        { }

        /// <summary>
        /// Proc that is threadsafe.
        /// </summary>
        /// <param name="prc">proc to make threadsafe</param>
        /// <param name="lck">object to lock threadsafe</param>
        public SyncAction(IAction prc, object lck)
        {
            this.proc = prc;
            this.lck = lck;
        }

        /// <summary>
        /// Execute procedure with given input.
        /// </summary>
        public void Invoke()
        {
            lock (this.lck)
            {
                this.proc.Invoke();
            }
        }

    }
}
