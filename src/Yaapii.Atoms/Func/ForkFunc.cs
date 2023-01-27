using System;
using System.Collections.Generic;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Map;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// An action that is chosen by given condition.
    /// </summary>
    public sealed class ForkFunc<TInput, TOutcome> : IFunc<TInput, TOutcome>
    {
        private readonly FallbackMap<TInput, Func<TOutcome>> map;
        /// <summary>
        /// An action that is chosen by given condition.
        /// </summary>
        public ForkFunc(Tooth<string, bool> tooth, Func<TInput, Func<TOutcome>> fallback, params IKvp<TInput, Func<TOutcome>>[] tooths) : this(
            new ManyOf<IKvp<TInput, Func<TOutcome>>>(tooths),
            fallback
        )
        { }
        /// <summary>
        /// An action that is chosen by given condition.
        /// </summary>
        public ForkFunc(params IKvp<TInput, Func<TOutcome>>[] tooths) : this(
            new ManyOf<IKvp<TInput, Func<TOutcome>>>(tooths),
            (missing) => throw new ArgumentException($"There is no action linked to given '{missing}'")
        )
        { }

        /// <summary>
        /// An action that is chosen by given condition.
        /// </summary>
        public ForkFunc(IEnumerable<IKvp<TInput, Func<TOutcome>>> tooths, Func<TInput, Func<TOutcome>> fallback)
        {
            this.map =
                new FallbackMap<TInput, Func<TOutcome>>(
                    new MapOf<TInput, Func<TOutcome>>(tooths),
                    fallback
                );
        }

        public TOutcome Invoke(TInput condition)
        {
            return this.map[condition]
                .Invoke();
        }
    }

    /// <summary>
    /// A tooth for a <see cref="ForkAction{T}"/>
    /// </summary>
    public sealed class Tooth<TInput, TOutcome> : KvpEnvelope<TInput, Func<TOutcome>>
    {
        /// <summary>
        /// A tooth for a <see cref="ForkAction{T}"/>
        /// </summary>
        public Tooth(TInput condition, Func<TOutcome> consequence) : base(new KvpOf<TInput, Func<TOutcome>>(condition, consequence))
        { }
    }
}
