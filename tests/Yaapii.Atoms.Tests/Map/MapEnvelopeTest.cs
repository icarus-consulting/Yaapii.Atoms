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

using System;
using System.Collections.Generic;
using Xunit;

namespace Yaapii.Atoms.Lookup.Tests
{
    public class MapEnvelopeTest
    {
        [Fact]
        public void TryGetValueWithExistingKey()
        {

            var map = new NonAbstractEnvelope(new Dictionary<int, int> { { 7, 42 } });
            int outValue;
            Assert.True(map.TryGetValue(7, out outValue));
            Assert.Equal(42, outValue);
        }

        [Fact]
        public void TryGetValueWithMissingKey()
        {
            var map = new NonAbstractEnvelope(new Dictionary<int, int> { { 7, 42 } });
            int outValue;
            Assert.False(map.TryGetValue(0, out outValue));
        }

        [Fact]
        public void GetValueWithExistingKey()
        {
            var map = new NonAbstractEnvelope(new Dictionary<int, int> { { 7, 42 } });
            var outValue = map[7];
            Assert.Equal(42, outValue);
        }

        [Fact]
        public void GetValueWithMissingKey()
        {
            var map = new NonAbstractEnvelope(new Dictionary<int, int> { { 7, 42 } });

            try
            {
                var outValue = map[0];
                Assert.True(false, "This has to fail");
            }
            catch (ArgumentException ex)
            {
                Assert.Equal("The key '0' is not present in the map.", ex.Message);
            }
        }

        [Fact]
        public void GetValueWithFallbackAndExistingKey()
        {
            var map = new NonAbstractEnvelope(new Dictionary<int, int> { { 7, 42 } }, key => key * 2);
            var outValue = map[7];
            Assert.Equal(42, outValue);
        }

        [Fact]
        public void GetValueWithFallbackAndMissingKey()
        {
            var map = new NonAbstractEnvelope(new Dictionary<int, int> { { 7, 42 } }, key => key * 2);
            var outValue = map[2];
            Assert.Equal(4, outValue);
        }

        private class NonAbstractEnvelope : Map.Envelope<int, int>
        {
            public NonAbstractEnvelope(IDictionary<int, int> map) : base(() => map)
            { }

            public NonAbstractEnvelope(IDictionary<int, int> map, Func<int, int> fallback) : base(() => map, fallback)
            { }
        }
    }
}
