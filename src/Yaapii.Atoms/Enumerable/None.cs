using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// Enumerable which is empty.
    /// </summary>
    public sealed class None : ManyEnvelope<string>
    {
        /// <summary>
        /// Enumerable which is empty.
        /// </summary>
        public None() : base(() => new ManyOf<string>(), false)
        { }
    }

    /// <summary>
    /// Enumerable which is empty.
    /// </summary>
    public sealed class None<T> : ManyEnvelope<T>
    {
        /// <summary>
        /// Enumerable which is empty.
        /// </summary>
        public None() : base(() => new ManyOf<T>(), false)
        { }
    }
}
