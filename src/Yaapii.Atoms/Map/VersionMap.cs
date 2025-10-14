// MIT License
//
// Copyright(c) 2025 ICARUS Consulting GmbH
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
using System.Collections;
using System.Collections.Generic;
using Yaapii.Atoms.Enumerable;

#pragma warning disable NoProperties // No Properties
#pragma warning disable MaxPublicMethodCount // a public methods count maximum

namespace Yaapii.Atoms.Map
{
    /// <summary>
    /// A map which matches a version. 
    /// It can match the version range, not the exact version.
    /// This means if you have two krvps inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
    /// </summary>
    public sealed class VersionMap : MapEnvelope<Version, string>
    {
        public VersionMap(bool openEnd, params IKvp<Version, string>[] kvps) : this(new ManyOf<IKvp<Version, string>>(kvps), openEnd)
        { }

        public VersionMap(IEnumerable<IKvp<Version, string>> kvps, bool openEnd) : base(() => new VersionMap<string>(kvps, openEnd), false)
        { }
    }

    /// <summary>
    /// A dictionary which matches a version. 
    /// It matches the version range, not the exact version.
    /// This means if you have two kvps inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
    /// </summary>
    /// <typeparam name="Value"></typeparam>
    public sealed class VersionMap<Value> : IDictionary<Version, Value>
    {
        private readonly IDictionary<Version, Value> map;
        private readonly InvalidOperationException reject = new InvalidOperationException("Not supported, this is only a lookup map for versions.");
        private readonly bool openEnd;
        private readonly Func<Version, IEnumerable<Version>, InvalidOperationException> versionNotFound =
            (version, available) =>
            new InvalidOperationException(
                $"Cannot find value for version {version.ToString()}, the version must be within: "
                + new Text.Joined(", ",
                    new Mapped<Version, string>(
                        v => v.ToString(),
                        available
                    )
                ).AsString()
            );

        /// <summary>
        /// A dictionary which matches a version. 
        /// It matches the version range, not the exact version.
        /// This means if you have two kvps inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
        /// </summary>
        public VersionMap(params IKvp<Version, Value>[] kvps) : this(false, kvps)
        { }

        /// <summary>
        /// A dictionary which matches a version. 
        /// It matches the version range, not the exact version.
        /// This means if you have two kvps inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
        /// </summary>
        public VersionMap(bool openEnd, params IKvp<Version, Value>[] kvps) : this(new ManyOf<IKvp<Version, Value>>(kvps), openEnd)
        { }

        /// <summary>
        /// A dictionary which matches a version. 
        /// It matches the version range, not the exact version.
        /// This means if you have two kvps inside: 1.0 and 3.0, and your key is 2.0, the version 1.0 is matched.
        /// </summary>
        public VersionMap(IEnumerable<IKvp<Version, Value>> kvps, bool openEnd)
        {
            this.map = new LazyDict<Version, Value>(kvps);
            this.openEnd = openEnd;
        }

        public Value this[Version key] { get => this.Match(key); set => throw this.reject; }

        public ICollection<Version> Keys => this.map.Keys;

        public ICollection<Value> Values => this.map.Values;

        public int Count => this.map.Count;

        public bool IsReadOnly => true;

        public void Add(Version key, Value value)
        {
            throw this.reject;
        }

        public void Add(KeyValuePair<Version, Value> item)
        {
            throw this.reject;
        }

        public void Clear()
        {
            throw this.reject;
        }

        public bool Contains(KeyValuePair<Version, Value> item)
        {
            return this.map.Contains(item);
        }

        public bool ContainsKey(Version key)
        {
            var result = false;
            try
            {
                var value = this.Match(key);
                result = true;
            }
            catch (Exception)
            {

            }
            return result;
        }

        public void CopyTo(KeyValuePair<Version, Value>[] array, int arrayIndex)
        {
            throw this.reject;
        }

        public IEnumerator<KeyValuePair<Version, Value>> GetEnumerator()
        {
            return this.map.GetEnumerator();
        }

        public bool Remove(Version key)
        {
            throw this.reject;
        }

        public bool Remove(KeyValuePair<Version, Value> item)
        {
            throw this.reject;
        }

        public bool TryGetValue(Version key, out Value value)
        {
            var result = false;
            value = default(Value);
            try
            {
                value = this.Match(key);
                result = true;
            }
            catch (Exception) { }
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private Value Match(Version candidate)
        {
            var prettyCandidate = new Version(
                candidate.Major,
                candidate.Minor,
                candidate.Build == -1 ? 0 : candidate.Build,
                candidate.Revision == -1 ? 0 : candidate.Revision
            );
            var match = new Version(0, 0);
            var matched = false;
            foreach (var lowerBound in this.map.Keys)
            {
                if (prettyCandidate >= lowerBound)
                {
                    match = lowerBound;
                    matched = true;
                }
                else if (match < prettyCandidate)
                {
                    break;
                }
            }

            if (matched)
            {
                if (this.openEnd || new List<Version>(this.map.Keys).IndexOf(match) < this.map.Keys.Count - 1)
                {
                    return this.map[match];
                }
                else
                {
                    throw this.versionNotFound(prettyCandidate, this.map.Keys);
                }
            }
            throw this.versionNotFound(prettyCandidate, this.map.Keys);
        }
    }
}
