using System;
using System.Collections.Generic;
using Yaapii.Atoms.Dict;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Map
{
    /// <summary>
    /// A key to many strings.
    /// Since 1.9.2019
    /// </summary>
    public sealed class KeyToMany : KvpEnvelope<IEnumerable<string>>
    {
        /// <summary>
        /// A key to many strings.
        /// The functions are executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public KeyToMany(string key, params Func<string>[] many) : this(key, () =>
            {
                var lst = new List<string>();
                for (var i = 0; i < many.Length; i++)
                {
                    lst.Add(many[i]());
                }
                return lst;
            })
        { }

        /// <summary>
        /// A key to many strings.
        /// The functions are executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public KeyToMany(string key, params string[] many) : this(key, () => new Strings(many))
        { }

        /// <summary>
        /// A key to many strings.
        /// </summary>
        public KeyToMany(string key, IEnumerable<string> many) : this(key, () => many)
        { }

        /// <summary>
        /// A key to many strings.
        /// The function is executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public KeyToMany(string key, Func<IEnumerable<string>> many) : base(
            new KvpOf<IEnumerable<string>>(key, many)
        )
        { }
    }

    /// <summary>
    /// A key to many values.
    /// </summary>
    public sealed class KeyToMany<TValue> : KvpEnvelope<IEnumerable<TValue>>
    {
        /// <summary>
        /// A key to many strings.
        /// The functions are executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public KeyToMany(string key, params Func<TValue>[] many) : this(key, () =>
            {
                var lst = new List<TValue>();
                for (var i = 0; i < many.Length; i++)
                {
                    lst.Add(many[i]());
                }
                return lst;
            }
        )
        { }

        /// <summary>
        /// A key to many strings.
        /// The functions are executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public KeyToMany(string key, params TValue[] many) : this(key, () =>
            {
                var lst = new List<TValue>();
                for (var i = 0; i < many.Length; i++)
                {
                    lst.Add(many[i]);
                }
                return lst;
            }
        )
        { }

        /// <summary>
        /// A key to many strings.
        /// </summary>
        public KeyToMany(string key, IEnumerable<TValue> many) : this(key, () => many)
        { }

        /// <summary>
        /// A key to many values.
        /// </summary>
        public KeyToMany(string key, Func<IEnumerable<TValue>> many) : base(
            new KvpOf<IEnumerable<TValue>>(key, many)
        )
        { }
    }

    /// <summary>
    /// A key to many values.
    /// </summary>
    public sealed class KeyToMany<TKey, TValue> : KvpEnvelope<TKey, IEnumerable<TValue>>
    {
        /// <summary>
        /// A key to many strings.
        /// The functions are executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public KeyToMany(TKey key, params Func<TValue>[] many) : this(key, () =>
        {
            var lst = new List<TValue>();
            for (var i = 0; i < many.Length; i++)
            {
                lst.Add(many[i]());
            }
            return lst;
        }
        )
        { }

        /// <summary>
        /// A key to many strings.
        /// The functions are executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public KeyToMany(TKey key, params TValue[] many) : this(key, () =>
        {
            var lst = new List<TValue>();
            for (var i = 0; i < many.Length; i++)
            {
                lst.Add(many[i]);
            }
            return lst;
        }
        )
        { }

        /// <summary>
        /// A key to many strings.
        /// </summary>
        public KeyToMany(TKey key, IEnumerable<TValue> many) : this(key, () => many)
        { }

        /// <summary>
        /// A key to many values.
        /// </summary>
        public KeyToMany(TKey key, Func<IEnumerable<TValue>> many) : base(
            new KvpOf<TKey, IEnumerable<TValue>>(key, many)
        )
        { }
    }
}
