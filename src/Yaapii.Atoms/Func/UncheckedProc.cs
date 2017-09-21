using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Proc that does not throw <see cref="IOException"/> but throws <see cref="UncheckedIOException"/> instead.
    /// </summary>
    /// <typeparam name="X"></typeparam>
    public sealed class UncheckedProc<X> : IProc<X>
    {
        private readonly IProc<X> _proc;

        /// <summary>
        /// Func that does not throw <see cref="IOException"/> but throws <see cref="UncheckedIOException"/> instead.
        /// </summary>
        /// <param name="prc"></param>
        public UncheckedProc(IProc<X> prc)
        {
            this._proc = prc;
        }

        public void Exec(X input)
        {
            new UncheckedFunc<X, Boolean>(new FuncOf<X, Boolean>(ipt => { this._proc.Exec(ipt); return true; })).Invoke(input);
        }

    }
}
