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

using System;
using System.Collections.Generic;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Map
{
    /// <summary>
    /// Simplified MapInput building.
    /// Since 9.9.2019
    /// </summary>
    public abstract class MapInputEnvelope : IMapInput
    {
        private readonly Func<IDictionary<string, string>, IDictionary<string, string>> apply;

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(Func<IMapInput> input) : this(
            dict => new Joined(dict, new MapOf(input()))
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(IScalar<IMapInput> input) : this(
            dict => new Joined(dict, new MapOf(input.Value()))
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(params IKvp[] kvps) : this(
            new LiveMany<IKvp>(kvps)
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(System.Collections.Generic.IEnumerable<IKvp> kvps) : this(
            input => new Joined(input, new LazyDict(kvps))
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(IDictionary<string, string> dict) : this(
            input => new Joined(input, dict)
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(Func<IDictionary<string, string>, IDictionary<string, string>> apply)
        {
            this.apply = apply;
        }

        /// <summary>
        /// Apply this input to a map.
        /// </summary>
        public IDictionary<string, string> Apply(IDictionary<string, string> dict)
        {
            return this.apply.Invoke(dict);
        }

        /// <summary>
        /// Return this as IMapInput.
        /// </summary>
        /// <returns></returns>
        public IMapInput Self()
        {
            return this;
        }
    }

    /// <summary>
    /// Simplified MapInput building.
    /// Since 9.9.2019
    /// </summary>
    public abstract class MapInputEnvelope<Value> : IMapInput<Value>
    {
        private readonly Func<IDictionary<string, Value>, IDictionary<string, Value>> apply;

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(Func<IMapInput<Value>> input) : this(
            dict => new Joined<Value>(dict, new MapOf<Value>(input()))
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(IScalar<IMapInput<Value>> input) : this(
            dict => new Joined<Value>(dict, new MapOf<Value>(input.Value()))
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(params IKvp<Value>[] kvps) : this(
            new LiveMany<IKvp<Value>>(kvps)
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(System.Collections.Generic.IEnumerable<IKvp<Value>> kvps) : this(
            input => new Joined<Value>(input, new LazyDict<Value>(kvps, false))
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(IDictionary<string, Value> dict) : this(
            input => new Joined<Value>(input, dict)
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(Func<IDictionary<string, Value>, IDictionary<string, Value>> apply)
        {
            this.apply = apply;
        }

        /// <summary>
        /// Apply this input to a map.
        /// </summary>
        public IDictionary<string, Value> Apply(IDictionary<string, Value> dict)
        {
            return this.apply.Invoke(dict);
        }

        /// <summary>
        /// Return this as IMapInput.
        /// </summary>
        /// <returns></returns>
        public IMapInput<Value> Self()
        {
            return this;
        }
    }

    /// <summary>
    /// Simplified MapInput building.
    /// Since 9.9.2019
    /// </summary>
    public abstract class MapInputEnvelope<Key, Value> : IMapInput<Key, Value>
    {
        private readonly Func<IDictionary<Key, Value>, IDictionary<Key, Value>> apply;

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(Func<IMapInput<Key, Value>> input) : this(
            dict => new Joined<Key, Value>(dict, new MapOf<Key, Value>(input()))
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(IScalar<IMapInput<Key, Value>> input) : this(
            dict => new Joined<Key, Value>(dict, new MapOf<Key, Value>(input.Value()))
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(params IKvp<Key, Value>[] kvps) : this(
            new LiveMany<IKvp<Key, Value>>(kvps)
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(System.Collections.Generic.IEnumerable<IKvp<Key, Value>> kvps) : this(
            input => new Joined<Key, Value>(input, new LazyDict<Key, Value>(kvps, false))
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(IDictionary<Key, Value> dict) : this(
            input => new Joined<Key, Value>(input, dict)
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(Func<IDictionary<Key, Value>, IDictionary<Key, Value>> apply)
        {
            this.apply = apply;
        }

        /// <summary>
        /// Apply this input to a map.
        /// </summary>
        public IDictionary<Key, Value> Apply(IDictionary<Key, Value> dict)
        {
            return this.apply.Invoke(dict);
        }

        /// <summary>
        /// Return this as IMapInput.
        /// </summary>
        /// <returns></returns>
        public IMapInput<Key, Value> Self()
        {
            return this;
        }
    }
}
