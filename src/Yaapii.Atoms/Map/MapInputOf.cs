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
using Yaapii.Atoms.List;

namespace Yaapii.Atoms.Map
{
    /// <summary>
    /// MapInput from key-value pairs.
    /// Since 9.9.2019
    /// </summary>
    public sealed class MapInputOf : MapInputEnvelope
    {
        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public MapInputOf(params IKvp[] kvps) : this(new ListOf<IKvp>(kvps))
        { }

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public MapInputOf(IEnumerable<IKvp> kvps) : base(kvps)
        { }

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public static IMapInput<Value> New<Value>(params IKvp<Value>[] kvps)
            => new MapInputOf<Value>(kvps);

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public static IMapInput<Value> New<Value>(IEnumerable<IKvp<Value>> kvps)
            => new MapInputOf<Value>(kvps);

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public static IMapInput<Key, Value> New<Key, Value>(params IKvp<Key, Value>[] kvps)
            => new MapInputOf<Key, Value>(kvps);

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public static IMapInput<Key, Value> New<Key, Value>(IEnumerable<IKvp<Key, Value>> kvps)
            => new MapInputOf<Key, Value>(kvps);
    }

    /// <summary>
    /// MapInput from key-value pairs.
    /// Since 9.9.2019
    /// </summary>
    public sealed class MapInputOf<Value> : MapInputEnvelope<Value>
    {
        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public MapInputOf(params IKvp<Value>[] kvps) : this(new ListOf<IKvp<Value>>(kvps))
        { }

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public MapInputOf(IEnumerable<IKvp<Value>> kvps) : base(kvps)
        { }
    }

    /// <summary>
    /// MapInput from key-value pairs.
    /// Since 9.9.2019
    /// </summary>
    public sealed class MapInputOf<Key, Value> : MapInputEnvelope<Key, Value>
    {
        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public MapInputOf(params IKvp<Key, Value>[] kvps) : this(new ListOf<IKvp<Key, Value>>(kvps))
        { }

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public MapInputOf(IEnumerable<IKvp<Key, Value>> kvps) : base(kvps)
        { }
    }
}
