using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Collection;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.List
{
    public sealed class Joined<T> : ListEnvelope<T>
    {

        public Joined(params List<T>[] src) : this(new EnumerableOf<List<T>>(src))
        { }

        public Joined(IEnumerable<List<T>> src) : base(() =>
            {
                var blocking = new BlockingCollection<T>();
                foreach (var lst in src)
                {
                    lst.AddRange(lst);
                }

                return new ListOf<T>(blocking);
                
            })
        { }
    }
}
