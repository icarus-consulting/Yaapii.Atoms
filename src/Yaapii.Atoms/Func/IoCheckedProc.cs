using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Proc that does not throw <see cref="Exception"/> but throws <see cref="IOException"/> instead
    /// </summary>
    /// <typeparam name="X"></typeparam>
    public sealed class IoCheckedProc<X> : IProc<X>
    {
        /// <summary>
        /// original proc
        /// </summary>
        private readonly IProc<X> _proc;

        /// <summary>
        /// Proc that does not throw <see cref="Exception"/> but throws <see cref="IOException"/> instead.
        /// </summary>
        /// <param name="prc">procedure to execute</param>
        public IoCheckedProc(IProc<X> prc)
        {
            this._proc = prc;
        }

        public void Exec(X input)
        {
            new IoCheckedFunc<X, Boolean>(
                new FuncOf<X, Boolean>((arg) =>
                 {
                     this._proc.Exec(arg);
                     return true;
                 })
            ).Invoke(input);
        }

    }
}
