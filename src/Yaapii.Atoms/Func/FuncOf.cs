
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms;
using Yaapii.Atoms.Misc;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Function that has input and output
    /// </summary>
    /// <typeparam name="X">input</typeparam>
    /// <typeparam name="Y">output</typeparam>
    public sealed class FuncOf<X, Y> : IFunc<X, Y>
    {
        private readonly System.Func<X, Y> _func;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="callable"></param>
        public FuncOf(System.Func<Y> callable) : this(input => callable.Invoke())
        { }

        /// <summary>
        /// Function that has input and output
        /// </summary>
        /// <param name="proc">procedure to execute</param>
        /// <param name="result"></param>
        public FuncOf(IProc<X> proc, Y result) : this(
                input =>
                {
                    proc.Exec(input);
                    return result;
                }
        )
        { }

        /// <summary>
        /// Function that has input and output
        /// </summary>
        /// <param name="func">function to execute</param>
        public FuncOf(System.Func<X, Y> func)
        {
            _func = func;
        }

        /// <summary>
        /// Generate the Output from the input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Y Invoke(X input)
        {
            return _func.Invoke(input);
        }
    }
}
