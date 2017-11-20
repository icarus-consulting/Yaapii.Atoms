using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.List
{
    /// <summary>
    /// A list that is both sticky and threadsafe.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class SolidList<T> : ListEnvelope<T>
    {

        public SolidList(params T[] items) : this(new EnumerableOf<T>(items))
        { }

        public SolidList(IEnumerable<T> items) : this(new ListOf<T>(items))
        { }

        public SolidList(IEnumerator<T> items) : this(new ListOf<T>(items))
        { }

        public SolidList(ICollection<T> list) : base(
            () => new SyncList<T>(
                    new StickyList<T>(
                        new ListOf<T>(list))))
        { }

    }
}
