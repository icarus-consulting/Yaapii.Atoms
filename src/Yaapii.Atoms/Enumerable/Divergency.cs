using System;
using System.Collections.Generic;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// Items which do only exist in one enumerable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Divergency<T> : ManyEnvelope<T>
    {
        /// <summary>
        /// Items which do only exist in one enumerable.
        /// </summary>
        public Divergency(IEnumerable<T> a, IEnumerable<T> b, Func<T, bool> match) : base(() =>
            {
                var result = new List<T>();
                foreach (var item in b)
                {
                    if (new Contains<T>(a, match).Value() != new Contains<T>(b, match).Value())
                    {
                        result.Add(item);
                    }
                }
                return result;
            },
            false
        )
        { }

        /// <summary>
        /// Items which do only exist in one enumerable.
        /// </summary>
        public Divergency(IEnumerable<T> a, IEnumerable<T> b) : base(() =>
            {
                var result = new List<T>();
                foreach (var item in new Joined<T>(a, b))
                {
                    if (new Contains<T>(a, item).Value() != new Contains<T>(b, item).Value())
                    {
                        result.Add(item);
                    }
                }
                return result;
            },
            false
        )
        { }

        /// <summary>
        /// Items which do only exist in one enumerable.
        /// </summary>
        private Divergency(Func<IEnumerable<T>> unite) : base(unite, false)
        { }
    }
}