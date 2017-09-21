using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Action with input but no output as runnable.
    /// </summary>
    /// <typeparam name="In">type of input</typeparam>
    public sealed class RunnableOf<In> : IAction
    {
        private readonly System.Action<In> _func;
        private readonly In _input;

        /// <summary>
        /// Action with input but no output as runnable.
        /// </summary>
        /// <param name="fnc"></param>
        /// <param name="ipt"></param>
        public RunnableOf(System.Action<In> fnc, In ipt)
        {
            this._func = fnc;
            this._input = ipt;
        }

        public void Run()
        {
            new UncheckedFunc<In, bool>(
                new FuncOf<In, bool>((input) =>
                    {
                        this._func.Invoke(this._input);
                        return true;
                    })).Invoke(this._input);
        }
    }
}
