using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.List;

#pragma warning disable NoGetOrSet
namespace Yaapii.EnumerableDeck
{
    /// <summary>
    /// A <see cref="IEnumerable"/> whose items are replaced if they match a condition.
    /// </summary>
    /// <typeparam name="T">type of items in enumerable</typeparam>
    public sealed class Replaced<T> : IEnumerable<T>
    {
        private readonly List<T> _origin;
        private readonly T _replacement;
        private readonly IFunc<T, bool> _match;

        /// <summary>
        /// A <see cref="IEnumerable"/> whose items are replaced if they match a condition.
        /// </summary>
        /// <param name="origin">enumerable</param>
        /// <param name="match">matching condition</param>
        /// <param name="replacement">item to insert instead</param>
        public Replaced(IEnumerable<T> origin, Func<T, bool> matches, T replacement) : this(origin, new FuncOf<T, bool>(matches), replacement)
        { }

        /// <summary>
        /// A <see cref="IEnumerable"/> whose items are replaced if they match a condition.
        /// </summary>
        /// <param name="origin">enumerable</param>
        /// <param name="match">matching condition</param>
        /// <param name="replacement">item to insert instead</param>
        public Replaced(IEnumerable<T> origin, IFunc<T, bool> condition, T replacement)
        {
            _origin = new List<T>(origin);
            _replacement = replacement;
            _match = condition;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Aggregated().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Aggregated().GetEnumerator();
        }

        private IEnumerable<T> Aggregated()
        {
            var result = new List<T>();

            for (int i = 0; i < _origin.Count;)
            {
                var original = _origin[i];

                if (_match.Invoke(original))
                {
                    result.Add(_replacement);
                    continue;
                }

                result.Add(original);
            }

            return result;
        }
    }
}
#pragma warning restore NoGetOrSet