using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Collection
{
    public sealed class StickyCollection<T> : CollectionEnvelope<T>
    {


        public StickyCollection(params T[] items) : this(new EnumerableOf<T>(items))
        { }


        public StickyCollection(IEnumerator<T> items) : this(new EnumerableOf<T>(items))
        { }

        public StickyCollection(IEnumerable<T> items) : this(new CollectionOf<T>(items))
        { }

        public StickyCollection(ICollection<T> list) : base(
                new StickyScalar<ICollection<T>>(
                    () =>
                    {
                        var temp = new List<T>(list.Count);
                        foreach(var item in list)
                        {
                            list.Add(item);
                        }
                        return new CollectionOf<T>(temp);
                    }
            ))
        { }
    }
}
