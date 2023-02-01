// MIT License
//
// Copyright(c) 2023 ICARUS Consulting GmbH
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Collections.Generic;
using System.Linq;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Function with two inputs which returns the output from cache.
    /// </summary>
    /// <typeparam name="In1">type of first argument</typeparam>
    /// <typeparam name="In2">type of second argument</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class StickyBiFunc<In1, In2, Out> : IBiFunc<In1, In2, Out>
    {
        /// <summary>
        /// original func
        /// </summary>
        private readonly IBiFunc<In1, In2, Out> func;

        /// <summary>
        /// cache
        /// </summary>
        private readonly Dictionary<Dictionary<In1, In2>, Out> cache;

        private readonly KeyMapComparer comparer;

        /// <summary>
        /// Function with two inputs which returns the output from cache.
        /// </summary>
        /// <param name="fnc">func to cache result from</param>
        public StickyBiFunc(System.Func<In1, In2, Out> fnc) : this(new BiFuncOf<In1, In2, Out>(fnc))
        { }

        /// <summary>
        /// Function with two inputs which returns the output from cache.
        /// </summary>
        /// <param name="fnc">func to cache result from</param>
        public StickyBiFunc(IBiFunc<In1, In2, Out> fnc)
        {
            this.func = fnc;
            this.comparer = new KeyMapComparer();
            this.cache = new Dictionary<Dictionary<In1, In2>, Out>(this.comparer);
        }

        /// <summary>
        /// Invoke the function and get the output.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public Out Invoke(In1 first, In2 second)
        {
            var keymap = new Dictionary<In1, In2>();
            keymap[first] = second;

            Out output;
            var km = new Filtered<Dictionary<In1, In2>>((key) => this.comparer.Equals(keymap, key), this.cache.Keys);
            if (km.Count() == 0)
            {
                output = this.func.Invoke(first, second);
                this.cache.Add(keymap, output);
                km = new Filtered<Dictionary<In1, In2>>((key) => this.comparer.Equals(keymap, key), this.cache.Keys);
            }

            return this.cache[new ItemAt<Dictionary<In1, In2>>(km).Value()];
        }

        private sealed class KeyMapComparer : IEqualityComparer<Dictionary<In1, In2>>
        {
            public bool Equals(Dictionary<In1, In2> x, Dictionary<In1, In2> y)
            {
                var equal = x.Keys.Count == y.Keys.Count;
                if (equal)
                {
                    for (var i = 0; i < x.Keys.Count; i++)
                    {
                        if (!x.Keys.ElementAt(i).Equals(y.Keys.ElementAt(i)))
                        {
                            equal = false;
                            break;
                        }
                    }
                }

                if (equal)
                {
                    foreach (var key in x.Keys)
                    {
                        if (!x[key].Equals(y[key]))
                        {
                            equal = false;
                            break;
                        }
                    }
                }
                return equal;
            }

            public int GetHashCode(Dictionary<In1, In2> obj)
            {
                return obj.GetHashCode();
            }
        }
    }

    public static class StickyBiFunc
    {
        /// <summary>
        /// Function with two inputs which returns the output from cache.
        /// </summary>
        /// <param name="fnc">func to cache result from</param>
        public static IBiFunc<In1, In2, Out> New<In1, In2, Out>(System.Func<In1, In2, Out> fnc) =>
            new StickyBiFunc<In1, In2, Out>(fnc);

        /// <summary>
        /// Function with two inputs which returns the output from cache.
        /// </summary>
        /// <param name="fnc">func to cache result from</param>
        public static IBiFunc<In1, In2, Out> New<In1, In2, Out>(IBiFunc<In1, In2, Out> fnc) =>
            new StickyBiFunc<In1, In2, Out>(fnc);
    }
}
