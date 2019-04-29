using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Enumerable
{
    public sealed class Strings : EnumerableEnvelope<string>
    {
        /// <summary>
        /// Strings made from the ToString method of given objects.
        /// </summary>
        /// <param name="strings"></param>
        public Strings(params object[] strings) : base(
            new ScalarOf<IEnumerable<string>>(
                new Mapped<object, string>(
                    s => s.ToString(), 
                    new EnumerableOf<object>(strings)
                )
            )
        )
        { }

        /// <summary>
        /// Enumerable of strings.
        /// </summary>
        /// <param name="strings"></param>
        public Strings(params string[] strings) : base(new ScalarOf<IEnumerable<string>>(new EnumerableOf<string>(strings)))
        { }
    }
}
