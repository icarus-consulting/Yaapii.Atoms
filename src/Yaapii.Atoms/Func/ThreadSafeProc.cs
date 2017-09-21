using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Proc that is threadsafe.
    /// </summary>
    /// <typeparam name="X"></typeparam>
    public sealed class ThreadSafeProc<In> : IProc<In>
    {
        /// <summary>
        /// original proc
        /// </summary>
        private readonly IProc<In> _proc;

        /// <summary>
        /// threadsafe-lock
        /// </summary>
        private readonly Object _lck;

        /// <summary>
        /// Proc that is threadsafe.
        /// </summary>
        /// <param name="prc">proc to make threadsafe</param>
        public ThreadSafeProc(IProc<In> prc) : this(prc, prc)
        { }

        /// <summary>
        /// Proc that is threadsafe.
        /// </summary>
        /// <param name="prc">proc to make threadsafe</param>
        /// <param name="lck">object to lock threadsafe</param>
        public ThreadSafeProc(IProc<In> prc, object lck)
        {
            this._proc = prc;
            this._lck = lck;
        }

        public void Exec(In input)
        {
            lock (this._lck)
            {
                this._proc.Exec(input);
            }
        }

    }
}
