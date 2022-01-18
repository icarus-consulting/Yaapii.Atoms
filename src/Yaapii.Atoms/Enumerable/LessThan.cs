using System;
using System.Collections;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// Tells if an enumerable has less than the specified items.
    /// </summary>
    public sealed class LessThan : ScalarEnvelope<bool>
    {
        /// <summary>
        /// Tells if an enumerable has less than the specified items.
        /// </summary>
        public LessThan(int amount, IEnumerable source) : base(() =>
        {
            if (amount < 0)
            {
                throw new ArgumentException($"A positive number is needed for amount (amount: {amount})");
            }
            var current = 0;
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext() && current <= amount)
            {
                current++;
            }
            return current < amount;
        })
        { }
    }
}
