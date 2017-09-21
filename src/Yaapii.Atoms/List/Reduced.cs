using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.List
{
    /// <summary>
    /// <see cref="IEnumerable{T}"/> whose items are reduced to one item using the given function.
    /// </summary>
    /// <typeparam name="InAndOut">input and output type</typeparam>
    /// <typeparam name="Element">type of elements in a list to reduce</typeparam>
    public sealed class Reduced<InAndOut, Element> : IScalar<InAndOut>
    {
        private readonly IEnumerable<Element> _enumerable;
        private readonly InAndOut _input;
        private readonly IBiFunc<InAndOut , Element, InAndOut> _func;

        /// <summary>
        /// <see cref="IEnumerable{Element}"/> whose items are reduced to one item using the given function.
        /// </summary>
        /// <param name="toReduce">enumerable to reduce</param>
        /// <param name="input">input for the reducing function</param>
        /// <param name="fnc">reducing function</param>
        public Reduced(IEnumerable<Element> toReduce, InAndOut input, Func<InAndOut, Element, InAndOut> fnc) : this(
            toReduce, input, new BiFuncOf<InAndOut, Element, InAndOut>((arg1, arg2) => fnc.Invoke(arg1, arg2)))
        { }

        /// <summary>
        /// <see cref="IEnumerable{Element}"/> whose items are reduced to one item using the given function.
        /// </summary>
        /// <param name="toReduce">enumerable to reduce</param>
        /// <param name="input">input for the reducing function</param>
        /// <param name="fnc">reducing function</param>
        public Reduced(IEnumerable<Element> toReduce, InAndOut input, IBiFunc<InAndOut, Element, InAndOut> fnc)
        {
            this._enumerable = toReduce;
            this._input = input;
            this._func = fnc;
        }

        public InAndOut Value()
        {
            InAndOut memo = this._input;
            foreach (Element item in this._enumerable)
            {
                memo = this._func.Apply(memo, item);
            }
            return memo;
        }

    }
}
