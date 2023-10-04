using System;
using System.Collections;
using System.Collections.Generic;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// Enumerable which memoizes already visited items.
    /// </summary>
    public class Sticky<T> : System.Collections.Generic.IEnumerable<T>
    {
        private readonly object exclusive;
        private readonly Lazy<IEnumerator<T>> source;
        private readonly bool[] ended;
        private readonly List<T> buffer;

        /// <summary>
        /// Enumerable which memoizes already visited items.
        /// </summary>
        public Sticky(System.Collections.Generic.IEnumerable<T> source) : this(() => source)
        { }

        /// <summary>
        /// Enumerable which memoizes already visited items.
        /// </summary>
        public Sticky(Func<System.Collections.Generic.IEnumerable<T>> source) : this(() => source().GetEnumerator())
        { }

        /// <summary>
        /// Enumerable which memoizes already visited items.
        /// </summary>
        public Sticky(IEnumerator<T> source) : this(() => source)
        { }

        /// <summary>
        /// Enumerable which memoizes already visited items.
        /// </summary>
        public Sticky(Func<IEnumerator<T>> source)
        {
            this.exclusive = new object();
            this.buffer = new List<T>();
            this.source = new Lazy<IEnumerator<T>>(() => source());
            this.ended = new bool[1] { false };
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (!ended[0])
            {
                foreach (var item in Partial())
                {
                    yield return item;
                }
            }
            else
            {
                foreach (var item in Full())
                {
                    yield return item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private System.Collections.Generic.IEnumerable<T> Partial()
        {
            var i = 0;
            var enumerator = this.source.Value;
            while (true)
            {
                var hasValue = default(bool);

                lock (exclusive)
                {
                    if (i >= buffer.Count)
                    {
                        hasValue = enumerator.MoveNext();
                        if (hasValue)
                            buffer.Add(enumerator.Current);
                    }
                    else
                    {
                        hasValue = true;
                    }
                }

                if (hasValue)
                    yield return buffer[i];
                else
                {
                    this.ended[0] = true;
                    break;
                }

                i++;
            }
        }

        private System.Collections.Generic.IEnumerable<T> Full()
        {
            foreach(var item in this.buffer)
            {
                yield return item;
            }
        }
    }

    public static class Sticky
    {
        /// <summary>
        /// Enumerable which memoizes already visited items.
        /// </summary>
        public static Sticky<T> New<T>(System.Collections.Generic.IEnumerable<T> source) =>
            new Sticky<T>(source);

        /// <summary>
        /// Enumerable which memoizes already visited items.
        /// </summary>
        public static Sticky<T> New<T>(Func<System.Collections.Generic.IEnumerable<T>> source) =>
            new Sticky<T>(source);

        /// <summary>
        /// Enumerable which memoizes already visited items.
        /// </summary>
        public static Sticky<T> New<T>(IEnumerator<T> source) =>
            new Sticky<T>(source);


        /// <summary>
        /// Enumerable which memoizes already visited items.
        /// </summary>
        public static Sticky<T> New<T>(Func<IEnumerator<T>> source) =>
            new Sticky<T>(source);
    }
}

