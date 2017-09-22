using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// A ternary operation.
    /// </summary>
    /// <typeparam name="In">type of input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class Ternary<In, Out> : IScalar<Out>
    {
        private readonly IScalar<Boolean> _condition;
        private readonly IScalar<Out> _consequent;
        private readonly IScalar<Out> _alternative;

        /// <summary>
        /// A ternary operation using the given input and functions.
        /// </summary>
        /// <param name="input">input</param>
        /// <param name="cnd">condition</param>
        /// <param name="cons">consequent</param>
        /// <param name="alter">alternative</param>
        public Ternary(In input, System.Func<In, Boolean> condition, System.Func<In, Out> consequent, System.Func<In, Out> alternative)
        :
            this(input,
                new FuncOf<In, Boolean>(condition),
                new FuncOf<In, Out>(consequent),
                new FuncOf<In, Out>(alternative))
        { }

        /// <summary>
        /// A ternary operation using the given input and functions.
        /// </summary>
        /// <param name="input">input</param>
        /// <param name="cnd">condition</param>
        /// <param name="cons">consequent</param>
        /// <param name="alter">alternative</param>
        public Ternary(In input, IFunc<In, Boolean> cnd,
            IFunc<In, Out> cons, IFunc<In, Out> alter)
        :
            this(
                new ScalarOf<bool>(() => cnd.Invoke(input)),
                new ScalarOf<Out>(() => cons.Invoke(input)),
                new ScalarOf<Out>(() => alter.Invoke(input))
            )
        { }

        /// <summary>
        /// A ternary operation using the given input and functions.
        /// </summary>
        /// <param name="cnd">condition</param>
        /// <param name="cons">consequent</param>
        /// <param name="alter">alternative</param>
        public Ternary(Boolean cnd, Out cons, Out alter) :
            this(new ScalarOf<Boolean>(cnd), cons, alter)
        { }

        /// <summary>
        /// A ternary operation using the given input and functions.
        /// </summary>
        /// <param name="cnd">condition</param>
        /// <param name="cons">consequent</param>
        /// <param name="alter">alternative</param>
        public Ternary(IScalar<Boolean> cnd, Out cons, Out alter) :
            this(cnd, new ScalarOf<Out>(cons), new ScalarOf<Out>(alter))
        { }

        /// <summary>
        /// A ternary operation using the given input and functions.
        /// </summary>
        /// <param name="cnd">condition</param>
        /// <param name="cons">consequent</param>
        /// <param name="alter">alternative</param>
        public Ternary(IScalar<Boolean> cnd, IScalar<Out> cons, IScalar<Out> alter)
        {
            this._condition = cnd;
            this._consequent = cons;
            this._alternative = alter;
        }

        public Out Value()
        {
            IScalar<Out> result;
            if (this._condition.Value())
            {
                result = this._consequent;
            }
            else
            {
                result = this._alternative;
            }
            return result.Value();
        }
    }
}
