using System;
using System.Collections.Generic;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// Union objects in two enumerables.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Divergency<T> : EnumerableEnvelope<T>
    {
        /// <summary>
        /// Union objects in two enumerables.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public Divergency(IEnumerable<T> a, IEnumerable<T> b, Func<T, bool> compare) : base(() =>
        {
            var result = new List<T>();
            foreach (var item in b)
            {
                if (new Contains<T>(a, item).Value() != new Contains<T>(b, item).Value())
                {
                    result.Add(item);
                }
            }
            return result;
        })
        { }

        /// <summary>
        /// Union objects in two enumerables.
        /// </summary>
        /// <typeparam name="T"></typeparam>
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
        })
        { }

        /// <summary>
        /// Union objects in two enumerables.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private Divergency(Func<IEnumerable<T>> unite) : base(unite)
        { }
    }
}