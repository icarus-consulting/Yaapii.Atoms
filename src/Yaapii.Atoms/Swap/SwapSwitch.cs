using System;
using System.Collections.Generic;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Map;

namespace Yaapii.Atoms.Swap
{
    /// <summary>
    /// A swap that is chosen by given condition.
    /// </summary>
    public sealed class SwapSwitch<TInput, TOutcome> : ISwap<TInput, TOutcome>
    {
        private readonly FallbackMap<TInput, Func<TOutcome>> map;
        /// <summary>
        /// A swap  that is chosen by given condition.
        /// </summary>
        public SwapSwitch(Func<TInput, Func<TOutcome>> fallback, params IKvp<TInput, Func<TOutcome>>[] tooths) : this(
            new ManyOf<IKvp<TInput, Func<TOutcome>>>(tooths),
            fallback
        )
        { }
        /// <summary>
        /// A swap  that is chosen by given condition.
        /// </summary>
        public SwapSwitch(params IKvp<TInput, Func<TOutcome>>[] tooths) : this(
            new ManyOf<IKvp<TInput, Func<TOutcome>>>(tooths),
            (missing) => throw new ArgumentException($"There is no action linked to given '{missing}'")
        )
        { }

        /// <summary>
        /// A swap  that is chosen by given condition.
        /// </summary>
        public SwapSwitch(IEnumerable<IKvp<TInput, Func<TOutcome>>> tooths, Func<TInput, Func<TOutcome>> fallback)
        {
            this.map =
                new FallbackMap<TInput, Func<TOutcome>>(
                    new MapOf<TInput, Func<TOutcome>>(tooths),
                    fallback
                );
        }

        public TOutcome Flip(TInput condition)
        {
            return this.map[condition]
                .Invoke();
        }
    }

    
}
