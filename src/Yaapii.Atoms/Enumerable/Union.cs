using System;
using System.Collections.Generic;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// Union objects in two enumerables.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Union<T> : EnumerableEnvelope<T>
    {
        /// <summary>
        /// Union objects in two enumerables.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public Union(IEnumerable<T> a, IEnumerable<T> b, Func<T, bool> compare) : base(() =>
        {
            var result = new List<T>();
            foreach (var item in b)
            {
                if (new Contains<T>(a, compare).Value())
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
        public Union(IEnumerable<T> a, IEnumerable<T> b) : base(() =>
        {
            var result = new List<T>();
            foreach (var item in b)
            {
                if (new Contains<T>(a, item).Value())
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
        private Union(Func<IEnumerable<T>> unite) : base(unite)
        { }
    }
}