using System;
using System.Collections.Generic;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Map
{
    /// <summary>
    /// A map from string to string.
    /// </summary>
    public sealed class StringMap : MapEnvelope<string, string>
    {
        /// <summary>
        /// A map from string to string.
        /// </summary>
        /// <param name="pairs">Pairs of mappings</param>
        public StringMap(Tuple<string, string>[] pairs) : this(
            new EnumerableOf<Tuple<string,string>>(pairs)
        )
        { }

        /// <summary>
        /// A map from string to string.
        /// </summary>
        /// <param name="pairs">Pairs of mappings</param>
        public StringMap(IEnumerable<Tuple<string, string>> pairs) : this(
            new Mapped<Tuple<string, string>, KeyValuePair<string, string>>(
                tpl => new KeyValuePair<string,string>(tpl.Item1, tpl.Item2),
                pairs
            )
        )
        { }

        /// <summary>
        /// A map from string to string.
        /// </summary>
        /// <param name="pairs">Pairs of mappings</param>
        public StringMap(params KeyValuePair<string, string>[] pairs) : this(
            new EnumerableOf<KeyValuePair<string, string>>(pairs)
        )
        { }

        /// <summary>
        /// A map from string to string.
        /// </summary>
        /// <param name="pairs">Pairs of mappings</param>
        public StringMap(IEnumerable<KeyValuePair<string, string>> pairs) : base(() =>
             new MapOf<string, string>(pairs)
        )
        { }

        /// <summary>
        /// A map from string to string.
        /// </summary>
        /// <param name="pairSequence">Pairs as a sequence, ordered like this: key-1, value-1, ... key-n, value-n</param>
        public StringMap(params string[] pairSequence) : this(new EnumerableOf<string>(pairSequence))
        { }

        /// <summary>
        /// A map from string to string.
        /// </summary>
        /// <param name="pairSequence">Pairs as a sequence, ordered like this: key-1, value-1, ... key-n, value-n</param>
        public StringMap(IEnumerable<string> pairSequence) : this(
            new ScalarOf<IList<string>>(
                new Atoms.List.ListOf<string>(pairSequence)
            )
        )
        { }

        /// <summary>
        /// A map from string to string.
        /// </summary>
        /// <param name="pairSequence">Pairs as a sequence, ordered like this: key-1, value-1, ... key-n, value-n</param>
        public StringMap(IScalar<IList<string>> pairSequence) : base(() =>
            {
                var pairs = pairSequence.Value();
                if (pairs.Count % 2 == 1)
                {
                    throw new ArgumentException("Cannot build string map from an odd number of pieces. Pieces must be in this order: key-1, value-1, key-n, value-n");
                }

                var result = new Dictionary<string, string>();
                for (int idx = 0; idx < pairs.Count; idx++)
                {
                    result.Add(pairSequence.Value()[idx], pairSequence.Value()[++idx]);
                }
                return result;
            })
        { }
    }
}
