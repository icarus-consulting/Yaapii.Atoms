using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Function that has two inputs and an output
    /// </summary>
    /// <typeparam name="In1">type of first input</typeparam>
    /// <typeparam name="In2">type of second input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class BiFuncOf<In1, In2, Out> : IBiFunc<In1, In2, Out>
    {
        private readonly System.Func<In1, In2, Out> _func;

        /// <summary>
        /// Function that has two inputs and an output.
        /// </summary>
        /// <param name="func"></param>
        public BiFuncOf(System.Func<In1, In2, Out> func)
        {
            _func = func;
        }

        public Out Apply(In1 arg1, In2 arg2)
        {
            return _func.Invoke(arg1, arg2);
        }
    }
}
