using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Lookup
{
    /// <summary>
    /// Fake Kvp
    /// </summary>
    public sealed class FkKvp<TKey, TValue> : IKvp<TKey, TValue>
    {
        private readonly Func<TKey> keyFunc;
        private readonly Func<TValue> valueFunc;
        private readonly Func<bool> isLazyFunc;

        /// <summary>
        /// Fake Kvp
        /// </summary>
        public FkKvp(Func<TKey> keyFunc, Func<TValue> valueFunc, Func<bool> isLazyFunc) {
            this.keyFunc = keyFunc;
            this.valueFunc = valueFunc;
            this.isLazyFunc = isLazyFunc;
        }

        public TValue Value()
        {
            return this.valueFunc();
        }

        public TKey Key()
        {
            return this.keyFunc();
        }

        public bool IsLazy()
        {
            return this.isLazyFunc();
        }
    }
}
