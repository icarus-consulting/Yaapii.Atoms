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
using Xunit;

namespace Yaapii.Atoms.Map.Tests
{
    public class FallbackMapTests
    {
        [Fact]
        public void TryGetValueWithExistingKey()
        {
            var map = new FallbackMap<int, int>(new Dictionary<int, int> { { 7, 42 } }, i => { throw new Exception("dont call fallback on tryGetValue"); });
            int outValue;
            Assert.True(map.TryGetValue(7, out outValue));
            Assert.Equal(42, outValue);
        }

        [Fact]
        public void TryGetValueWithMissingKey()
        {
            var map = new FallbackMap<int, int>(new Dictionary<int, int> { { 7, 42 } }, i => { throw new Exception("dont call fallback on tryGetValue"); });
            int outValue;
            Assert.False(map.TryGetValue(0, out outValue));
        }

        [Fact]
        public void GetsValueWithExistingKey()
        {
            var map = new FallbackMap<int, int>(new Dictionary<int, int> { { 7, 42 } }, i => { throw new Exception("dont call fallback when value exists"); });
            var outValue = map[7];
            Assert.Equal(42, outValue);
        }

        [Fact]
        public void GetsValueWithMissingKey()
        {
            var map = new FallbackMap<int, int>(new Dictionary<int, int> { { 7, 42 } }, key => key * 2);
            var outValue = map[2];
            Assert.Equal(4, outValue);
        }

        [Fact]
        public void GetsValueFromFallbackMap()
        {
            var map = new FallbackMap<int, int>(
                new Dictionary<int, int> { { 7, 42 } },
                new Dictionary<int, int> { { 13, 37 } }
            );

            var outValue = map[13];
            Assert.Equal(37, outValue);
        }

        [Fact]
        public void DoesNotGetValueWhenAlsoMissingInFallbackMap()
        {
            var map = new FallbackMap<int, int>(
                new Dictionary<int, int> { { 7, 42 } },
                new Dictionary<int, int> { { 13, 37 } }
            );

            Assert.Throws<KeyNotFoundException>(() => map[666]);
        }
    }
}
