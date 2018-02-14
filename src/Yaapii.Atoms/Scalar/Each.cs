using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// Does to all elements in a <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <typeparam name="In"></typeparam>
    public sealed class Each<In> : IAction
    {
        private readonly IAction<In> _action;
        private readonly IEnumerable<In> _enumerable;

        /// <summary>
        /// Logical and. Returns true after calling <see cref="IAction{X}"/> for every element.
        /// </summary>
        /// <param name="proc">the condition to apply</param>
        /// <param name="src">list of items</param>
        public Each(Action<In> proc, params In[] src) : this(new ActionOf<In>(input => proc.Invoke(input)), src)
        { }

        /// <summary>
        /// Logical and. Returns true after calling <see cref="IAction{X}"/> for every element.
        /// </summary>
        /// <param name="proc">the condition to apply</param>
        /// <param name="src">list of items</param>
        public Each(Action<In> proc, IEnumerable<In> src) : this(new ActionOf<In>(ipt => proc.Invoke(ipt)), src)
        { }

        /// <summary>
        /// Logical and. Returns true after calling <see cref="IAction{X}"/> for every element.
        /// </summary>
        /// <param name="proc">the condition to apply</param>
        /// <param name="src">list of items</param>
        public Each(IAction<In> proc, params In[] src) : this(
            proc, new EnumerableOf<In>(src))
        { }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="enumerable"></param>
        public Each(IAction<In> action, IEnumerable<In> enumerable)
        {
            _action = action;
            _enumerable = enumerable;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Invoke()
        {
            foreach (var item in _enumerable)
            {
                _action.Invoke(item);
            }
        }
    }
}
