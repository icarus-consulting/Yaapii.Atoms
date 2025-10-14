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
using System.Collections.Generic;
using Xunit;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Tests.Enumerable
{
    public sealed class ManyEnvelopeTests
    {
        [Fact]
        public void CachesWhenLiveIsFalse()
        {
            var enumerated = 0;
            var many = new MockMany<int>(
                Enumerating,
                false
            );
            foreach (var _ in many)
            { }
            foreach (var _ in many)
            { }
            Assert.Equal(1, enumerated);

            IEnumerable<int> Enumerating()
            {
                enumerated++;
                yield return 1;
                yield return 2;
                yield return 3;
            }
        }

        [Fact]
        public void DoesNotCacheWhenLiveIsTrue()
        {
            var enumerated = 0;
            var many = new MockMany<int>(
                Enumerating,
                true
            );
            foreach (var _ in many)
            { }
            foreach (var _ in many)
            { }
            Assert.Equal(2, enumerated);

            IEnumerable<int> Enumerating()
            {
                enumerated++;
                yield return 1;
                yield return 2;
                yield return 3;
            }
        }
    }

    public sealed class MockMany<T> : ManyEnvelope<T>
    {
        public MockMany(Func<IEnumerable<T>> source, bool live) : base(source, live)
        { }
    }
}
