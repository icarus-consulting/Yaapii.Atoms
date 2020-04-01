// MIT License
//
// Copyright(c) 2019 ICARUS Consulting GmbH
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

namespace Yaapii.Atoms.Lookup
{
    /// <summary>
    /// Simplification of Kvp-class-building.
    /// Since 9.9.2019
    /// </summary>
    public abstract class KvpEnvelope : IKvp
    {
        private readonly IKvp origin;

        public KvpEnvelope(IKvp origin)
        {
            this.origin = origin;
        }

        public string Key()
        {
            return this.origin.Key();
        }

        public string Value()
        {
            return this.origin.Value();
        }

        public bool IsLazy()
        {
            return this.origin.IsLazy();
        }
    }

    /// <summary>
    /// Simplification of Kvp building.
    /// Since 9.9.2019
    /// </summary>
    public abstract class KvpEnvelope<TValue> : IKvp<TValue>
    {
        private readonly IKvp<TValue> origin;

        /// <summary>
        /// Simplification of Kvp building
        /// </summary>
        public KvpEnvelope(IKvp<TValue> origin)
        {
            this.origin = origin;
        }

        public string Key()
        {
            return this.origin.Key();
        }

        public TValue Value()
        {
            return this.origin.Value();
        }

        public bool IsLazy()
        {
            return this.origin.IsLazy();
        }
    }

    /// <summary>
    /// Simplification of Kvp-class-building.
    /// Since 9.9.2019
    /// </summary>
    public abstract class KvpEnvelope<TKey, TValue> : IKvp<TKey, TValue>
    {
        private readonly IKvp<TKey, TValue> origin;

        /// <summary>
        /// Simplification of KVP building
        /// </summary>
        public KvpEnvelope(IKvp<TKey, TValue> origin)
        {
            this.origin = origin;
        }

        public TKey Key()
        {
            return this.origin.Key();
        }

        public TValue Value()
        {
            return this.origin.Value();
        }

        public bool IsLazy()
        {
            return this.origin.IsLazy();
        }
    }
}
