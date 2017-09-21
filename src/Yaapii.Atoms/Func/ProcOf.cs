using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Action as proc.
    /// </summary>
    /// <typeparam name="In"></typeparam>
    public sealed class ProcOf<In> : IProc<In>
    {
        /// <summary>
        /// the func
        /// </summary>
        private readonly System.Action<In> _func;

        /// <summary>
        /// Action as proc.
        /// </summary>
        /// <param name="action">action to execute</param>
        public ProcOf(Action action) : this((b) => { action.Invoke(); })
        { }

        /// <summary>
        /// Action as proc.
        /// </summary>
        /// <param name="fnc">action to execute</param>
        public ProcOf(System.Action<In> fnc)
        {
            this._func = fnc;
        }

        public void Exec(In input)
        {
            this._func.Invoke(input);
        }
    }
}
