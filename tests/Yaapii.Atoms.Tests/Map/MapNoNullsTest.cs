// MIT License
//
// Copyright(c) 2017 ICARUS Consulting GmbH
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
using System.Diagnostics;
using System.Text;
using System.Threading;
using Xunit;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Map.Tests
{
    public class MapNoNullTest
    {
        [Fact]
        public void ContainsWithExistingItem()
        {
            var map = new MapNoNulls<int, int>(
                new Dictionary<int, int> {
                    {0, 0}
                }
            );
            Assert.True(map.Contains(new KeyValuePair<int, int>(0, 0)));
        }

        [Fact]
        public void ContainsWithMissingItemKey()
        {
            var map = new MapNoNulls<int, int>(
                new Dictionary<int, int> {
                    {0, 0}
                }
            );
            Assert.False(map.Contains(new KeyValuePair<int, int>(1, 0)));
        }

        [Fact]
        public void ContainsWithMissingValue()
        {
            var map = new MapNoNulls<int, int>(
                new Dictionary<int, int> {
                    {0, 0}
                }
            );
            Assert.False(map.Contains(new KeyValuePair<int, int>(0, 1)));
        }

        [Fact]
        public void ContainsWithMissingItem()
        {
            var map = new MapNoNulls<int, int>(
                new Dictionary<int, int> {
                    {0, 0}
                }
            );
            Assert.False(map.Contains(new KeyValuePair<int, int>(1, 1)));
        }

        [Fact]
        public void ContainsWithNullKey()
        {
            var map = new MapNoNulls<object, object>(
                new Dictionary<object, object> {
                    {0, 0}
                }
            );
            Assert.Throws(typeof(ArgumentNullException), 
                () => map.Contains(new KeyValuePair<object, object>(null, 0))
            );
        }

        [Fact]
        public void ContainsWithNullValue()
        {
            var map = new MapNoNulls<object, object>(
                new Dictionary<object, object> {
                    {0, 0}
                }
            );
            Assert.Throws(typeof(ArgumentNullException), 
                () => map.Contains(new KeyValuePair<object, object>(0, null))
            );
        }

        [Fact]
        public void ContainsWithNullKeyAndNullValue()
        {
            var map = new MapNoNulls<object, object>(
                new Dictionary<object, object> {
                    {0, 0}
                }
            );
            Assert.Throws(typeof(ArgumentNullException), 
                () => map.Contains(new KeyValuePair<object, object>(null, null))
            );
        }

        [Fact]
        public void ContainsKeyWithExistingKey()
        {
            var map = new MapNoNulls<int, int>(
                new Dictionary<int, int> {
                    {0, 0}
                }
            );
            Assert.True(map.ContainsKey(0));
        }

        [Fact]
        public void ContainsKeyWithMissingKey()
        {
            var map = new MapNoNulls<int, int>(
                new Dictionary<int, int> {
                    {0, 0}
                }
            );
            Assert.False(map.ContainsKey(1));
        }

        [Fact]
        public void ContainsKeyWithNullKey()
        {
            var map = new MapNoNulls<object, object>(
                new Dictionary<object, object> {
                    {0, 0}
                }
            );
            Assert.Throws(typeof(ArgumentNullException), () => map.ContainsKey(null));
        }

        [Fact]
        public void AddEntry()
        {
            var map = new MapNoNulls<int, int>(new Dictionary<int, int>());
            map.Add(new KeyValuePair<int, int>(0, 0));
            Assert.Contains(new KeyValuePair<int, int>(0, 0), map);
        }

        [Fact]
        public void AddEntryWithNullKey()
        {
            var map = new MapNoNulls<object, object>(new Dictionary<object, object>());
            Assert.Throws(typeof(ArgumentNullException),
                () => map.Add(new KeyValuePair<object, object>(null, 0))
            );
        }

        [Fact]
        public void AddEntryWithNullValue()
        {
            var map = new MapNoNulls<object, object>(new Dictionary<object, object>());
            Assert.Throws(typeof(ArgumentNullException),
                () => map.Add(new KeyValuePair<object, object>(0, null))
            );
        }

        [Fact]
        public void AddEntryWithNullKeyAndNullValue()
        {
            var map = new MapNoNulls<object, object>(new Dictionary<object, object>());
            Assert.Throws(typeof(ArgumentNullException),
                () => map.Add(new KeyValuePair<object, object>(null, null))
            );
        }

        [Fact]
        public void AddKeyAndValue()
        {
            var map = new MapNoNulls<int, int>(new Dictionary<int, int>());
            map.Add(0, 0);
            Assert.Contains(new KeyValuePair<int, int>(0, 0), map);
        }

        [Fact]
        public void AddNullKeyAndValue()
        {
            var map = new MapNoNulls<object, object>(new Dictionary<object, object>());
            Assert.Throws(typeof(ArgumentNullException), () => map.Add(null, 0));
        }

        [Fact]
        public void AddKeyAndNullValue()
        {
            var map = new MapNoNulls<object, object>(new Dictionary<object, object>());
            Assert.Throws(typeof(ArgumentNullException), () => map.Add(0, null));
        }

        [Fact]
        public void AddNullKeyAndNullValue()
        {
            var map = new MapNoNulls<object, object>(new Dictionary<object, object>());
            Assert.Throws(typeof(ArgumentNullException), () => map.Add(null, null));
        }

        [Fact]
        public void TryGetValueWithExistingKey()
        {
            var map = new MapNoNulls<int, int>(
                new Dictionary<int, int> {
                    {0, 0}
                }
            );
            int outValue;
            Assert.True(map.TryGetValue(0, out outValue));
            Assert.Equal(0, outValue);
        }

        [Fact]
        public void TryGetValueWithMissingKey()
        {
            var map = new MapNoNulls<int, int>(
                new Dictionary<int, int> {
                    {0, 0}
                }
            );
            int outValue;
            Assert.False(map.TryGetValue(1, out outValue));
        }

        [Fact]
        public void TryGetValueWithNullKey()
        {
            var map = new MapNoNulls<object, object>(
                new Dictionary<object, object> {
                    {0, 0}
                }
            );
            object outValue;
            Assert.Throws(typeof(ArgumentNullException), () => map.TryGetValue(null, out outValue));
        }

        [Fact]
        public void GetValue()
        {
            var map = new MapNoNulls<int, int>(
                new Dictionary<int, int> {
                    {0, 0}
                }
            );
            Assert.Equal(0, map[0]);
        }

        [Fact]
        public void GetValueWithNullKey()
        {
            var map = new MapNoNulls<object, object>(
                new Dictionary<object, object> {
                    {0, 0}
                }
            );
            Assert.Throws(typeof(ArgumentNullException), () => map[null]);
        }

        [Fact]
        public void SetValue()
        {
            var map = new MapNoNulls<int, int>(
                new Dictionary<int, int> {
                    {0, 0}
                }
            );
            map[0] = 1;
            Assert.Equal(1, map[0]);
        }

        [Fact]
        public void SetValueWithNullKey()
        {
            var map = new MapNoNulls<object, object>(
                new Dictionary<object, object> {
                    {0, 0}
                }
            );
            Assert.Throws(typeof(ArgumentNullException), () => map[null]=1);
        }

        [Fact]
        public void SetValueWithNullValue()
        {
            var map = new MapNoNulls<object, object>(
                new Dictionary<object, object> {
                    {0, 0}
                }
            );
            Assert.Throws(typeof(ArgumentNullException), () => map[0]=null);
        }

        [Fact]
        public void SetValueWithNullKeyAndNullValue()
        {
            var map = new MapNoNulls<object, object>(
                new Dictionary<object, object> {
                    {0, 0}
                }
            );
            Assert.Throws(typeof(ArgumentNullException), () => map[null]=null);
        }
    }
}