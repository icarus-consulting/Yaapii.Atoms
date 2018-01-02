using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// A <see cref="ArrayList"/> converted to IEnumerable&lt;object&gt;
    /// </summary>
    public sealed class ArrayListAsEnumerable : EnumerableEnvelope<object>
    {
        /// <summary>
        /// A ArrayList converted to IEnumerable&lt;object&gt;
        /// </summary>
        /// <param name="src">source ArrayList</param>
        public ArrayListAsEnumerable(ArrayList src) : base(new StickyScalar<IEnumerable<object>>(() =>
        {
            var blocking = new BlockingCollection<object>();
            foreach (var lst in src)
            {
                new And<object>(item => blocking.Add(item), lst).Value();
            }

            return blocking;
        }))
        { }
    }
}
