using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Collection
{
    /// <summary>
    /// An ArrayList converted to a IList&lt;object&gt;
    /// </summary>
    public sealed class ArrayListAsCollection : CollectionEnvelope<object>
    {
        /// <summary>
        /// A ArrayList converted to IList&lt;object&gt;
        /// </summary>
        /// <param name="src">source ArrayList</param>
        public ArrayListAsCollection(ArrayList src) : base(new StickyScalar<ICollection<object>>(() =>
        {
            var blocking = new BlockingCollection<object>();
            foreach (var lst in src)
            {
                new And<object>(item => blocking.Add(item), lst).Value();
            }

            return new CollectionOf<object>(blocking.ToArray());
        }))
        { }
    }
}
