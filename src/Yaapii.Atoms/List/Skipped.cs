using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

#pragma warning disable NoGetOrSet // No Statics
namespace Yaapii.Atoms.List
{
    /// <summary>
    /// A <see cref="IEnumerable{Tests}"/> which skips a given count of items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Skipped<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _enumerable;
        private readonly int _skip;

        /// <summary>
        /// A <see cref="IEnumerable{Tests}"/> which skips a given count of items.
        /// </summary>
        /// <param name="enumerable">enumerable to skip items in</param>
        /// <param name="skip">how many to skip</param>
        public Skipped(IEnumerable<T> enumerable, int skip)
        {
            this._enumerable = enumerable;
            this._skip = skip;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new SkippedEnumerator<T>(this._enumerable.GetEnumerator(), this._skip);
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
#pragma warning restore NoGetOrSet // No Statics