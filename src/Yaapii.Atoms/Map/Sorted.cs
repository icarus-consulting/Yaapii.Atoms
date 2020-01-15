using System;
using System.Collections.Generic;

namespace Yaapii.Atoms.Lookup
{
    /// <summary>
    /// Sorts the given map with the given comparer
    /// </summary>
    /// <typeparam name="Key">Key Type of the Map</typeparam>
    /// <typeparam name="Value">Value Type of the Map</typeparam>
    public sealed class Sorted<Key, Value> : Map.Envelope<Key, Value>
    {
        /// <summary>
        /// Sorts the given map with the default comperator of the key
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        public Sorted(IEnumerable<KeyValuePair<Key, Value>> pairs)
            : this(pairs, Comparer<Key>.Default)
        { }

        /// <summary>
        /// Sorts the given map with the given compare function
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="compare">Function to compare two elements</param>
        public Sorted(IEnumerable<KeyValuePair<Key, Value>> pairs, Func<KeyValuePair<Key, Value>, KeyValuePair<Key, Value>, int> compare)
            : this(pairs, new ComparerFunc<KeyValuePair<Key, Value>>(compare))
        { }

        /// <summary>
        /// Sorts the given map with the given key comparer
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="cmp">Comparer comparing keys</param>
        public Sorted(IEnumerable<KeyValuePair<Key, Value>> pairs, IComparer<Key> cmp)
            : this(pairs, new KeyComparer<Key, Value>(cmp))
        { }

        /// <summary>
        /// Sorts the given map with the given comparer
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="cmp">Comparer comparing elements</param>
        public Sorted(IEnumerable<KeyValuePair<Key, Value>> pairs, IComparer<KeyValuePair<Key, Value>> cmp)
            : base(
                () =>
                {
                    var items = new List<KeyValuePair<Key, Value>>(pairs);
                    items.Sort(cmp);
                    return new Map.Of<Key, Value>(items);
                },
                true
            )
        { }
    }

    public sealed class Sorted<Value> : Map.Envelope<Value>
    {
        /// <summary>
        /// Sorts the given map with the default comperator of the key
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        public Sorted(IEnumerable<KeyValuePair<string, Value>> pairs)
            : this(pairs, Comparer<string>.Default)
        { }

        /// <summary>
        /// Sorts the given map with the given compare function
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="compare">Function to compare two elements</param>
        public Sorted(IEnumerable<KeyValuePair<string, Value>> pairs, Func<KeyValuePair<string, Value>, KeyValuePair<string, Value>, int> compare)
            : this(pairs, new ComparerFunc<KeyValuePair<string, Value>>(compare))
        { }

        /// <summary>
        /// Sorts the given map with the given key comparer
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="cmp">Comparer comparing keys</param>
        public Sorted(IEnumerable<KeyValuePair<string, Value>> pairs, IComparer<string> cmp)
            : this(pairs, new KeyComparer<string, Value>(cmp))
        { }

        /// <summary>
        /// Sorts the given map with the given comparer
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="cmp">Comparer comparing elements</param>
        public Sorted(IEnumerable<KeyValuePair<string, Value>> pairs, IComparer<KeyValuePair<string, Value>> cmp)
            : base(
                () =>
                {
                    var items = new List<KeyValuePair<string, Value>>(pairs);
                    items.Sort(cmp);
                    return new Map.Of<string, Value>(items);
                },
                true
            )
        { }
    }

    public sealed class Sorted : Map.Envelope
    {
        /// <summary>
        /// Sorts the given map with the default comperator of the key
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        public Sorted(IEnumerable<KeyValuePair<string, string>> pairs)
            : this(pairs, Comparer<string>.Default)
        { }

        /// <summary>
        /// Sorts the given map with the given compare function
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="compare">Function to compare two elements</param>
        public Sorted(IEnumerable<KeyValuePair<string, string>> pairs, Func<KeyValuePair<string, string>, KeyValuePair<string, string>, int> compare)
            : this(pairs, new ComparerFunc<KeyValuePair<string, string>>(compare))
        { }

        /// <summary>
        /// Sorts the given map with the given key comparer
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="cmp">Comparer comparing keys</param>
        public Sorted(IEnumerable<KeyValuePair<string, string>> pairs, IComparer<string> cmp)
            : this(pairs, new KeyComparer<string, string>(cmp))
        { }

        /// <summary>
        /// Sorts the given map with the given comparer
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="cmp">Comparer comparing elements</param>
        public Sorted(IEnumerable<KeyValuePair<string, string>> pairs, IComparer<KeyValuePair<string, string>> cmp)
            : base(
                () =>
                {
                    var items = new List<KeyValuePair<string, string>>(pairs);
                    items.Sort(cmp);
                    return new Map.Of<string, string>(items);
                },
                true
            )
        { }
    }

    internal sealed class ComparerFunc<T> : IComparer<T>
    {
        private readonly Func<T, T, int> compare;

        public ComparerFunc(Func<T, T, int> compare)
        {
            this.compare = compare;
        }

        public int Compare(T x, T y)
        {
            return this.compare(x, y);
        }
    }

    internal sealed class KeyComparer<Key, Value> : IComparer<KeyValuePair<Key, Value>>
    {
        private readonly IComparer<Key> cmp;

        public KeyComparer(IComparer<Key> cmp)
        {
            this.cmp = cmp;
        }

        public int Compare(KeyValuePair<Key, Value> x, KeyValuePair<Key, Value> y)
        {
            return this.cmp.Compare(x.Key, y.Key);
        }
    }
}
