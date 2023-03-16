using System;
using Yaapii.Atoms.Map;

namespace Yaapii.Atoms.Swap
{
    /// <summary>
    /// A SwapIf for a SwapSwitch
    /// </summary>
    public sealed class SwapIf<TInput, TOutcome> : KvpEnvelope<TInput, Func<TOutcome>>
    {
        /// <summary>
        /// A SwapIf for a SwapSwitch
        /// </summary>
        public SwapIf(TInput condition, Func<TOutcome> consequence) : base(new KvpOf<TInput, Func<TOutcome>>(condition, consequence))
        { }
    }
}
