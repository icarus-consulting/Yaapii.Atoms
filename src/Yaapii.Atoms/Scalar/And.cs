using System;
using System.Collections.Generic;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// Logical and. Returns true if all contents return true.
    /// </summary>
    /// <typeparam name="In"></typeparam>
    public sealed class And<In> : IScalar<Boolean>
    {
        private readonly IEnumerable<IScalar<Boolean>> _enumerable;

        /// <summary>
        /// Logical and. Returns true after calling <see cref="IProc{X}"/> for every element.
        /// </summary>
        /// <param name="proc">the condition to apply</param>
        /// <param name="src">list of items</param>
        public And(Action<In> proc, params In[] src) : this(new ProcOf<In>(input => proc.Invoke(input)), src)
        { }

        /// <summary>
        /// Logical and. Returns true after calling <see cref="IProc{X}"/> for every element.
        /// </summary>
        /// <param name="proc">the condition to apply</param>
        /// <param name="src">list of items</param>
        public And(IProc<In> proc, params In[] src) : this(
            proc, new EnumerableOf<In>(src))
        { }

        /// <summary>
        /// Logical and. Returns true after calling <see cref="IProc{X}"/> for every element.
        /// </summary>
        /// <param name="proc">the condition to apply</param>
        /// <param name="src">list of items</param>
        public And(IProc<In> proc, IEnumerable<In> src) : this(new FuncOf<In, bool>(input => { proc.Exec(input); return true; }), src)
        { }

        /// <summary>
        /// Logical and. Returns true if all calls to <see cref="Func{In, bool}" /> were true.
        /// </summary>
        /// <param name="func">the condition to apply</param>
        /// <param name="src">list of items</param>
        public And(System.Func<In, bool> func, params In[] src) : this(new FuncOf<In, bool>(func), new EnumerableOf<In>(src))
        { }

        /// <summary>
        /// Logical and. Returns true if all calls to <see cref="IFunc{In, Out}" /> were true.
        /// </summary>
        /// <param name="func">the condition to apply</param>
        /// <param name="src">list of items</param>
        public And(IFunc<In, Boolean> func, params In[] src) : this(func, new EnumerableOf<In>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="func">the condition to apply</param>
        /// <param name="src">list of items</param>
        public And(IFunc<In, Boolean> func, IEnumerable<In> src) :
            this(
                new Mapped<In, IScalar<Boolean>>(
                    src,
                        new FuncOf<In, IScalar<Boolean>>((item) => 
                        new ScalarOf<Boolean>(func.Invoke(item)))
                )
            )
        { }

        /// <summary>
        /// Logical and. Returns true if all calls to <see cref="Func{In, bool}" /> were true.
        /// </summary>
        /// <param name="func">the condition to apply</param>
        /// <param name="src">list of items</param>
        public And(params System.Func<bool>[] funcs) : this(new EnumerableOf<System.Func<bool>>(funcs))
        { }

        /// <summary>
        /// Logical and. Returns true if all calls to <see cref="Func{In, bool}" /> were true.
        /// </summary>
        /// <param name="func">the condition to apply</param>
        /// <param name="src">list of items</param>
        public And(EnumerableOf<System.Func<bool>> funcs) : this(
            new Mapped<System.Func<bool>, IScalar<bool>>(
                funcs, 
                func => new ScalarOf<bool>(func)))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">list of items</param>
        public And(params IScalar<Boolean>[] src) : this(
            new EnumerableOf<IScalar<Boolean>>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">list of items</param>
        public And(IEnumerable<IScalar<Boolean>> src)
        {
            this._enumerable = src;
        }

        public Boolean Value()
        {
            Boolean result = true;
            foreach (IScalar<Boolean> item in this._enumerable)
            {
                if (!item.Value())
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

    }
}
