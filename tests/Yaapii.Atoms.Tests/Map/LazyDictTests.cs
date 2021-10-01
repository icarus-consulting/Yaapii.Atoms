// MIT License
//
// Copyright(c) 2021 ICARUS Consulting GmbH
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
    public sealed class LazyDictTests
    {
        [Fact]
        public void AllValuesAreBuiltWhenPreventionIsDisabled()
        {
            var dict = new LazyDict<int, int>(false,
                new FkKvp<int, int>(() => 1, () => throw new Exception("i shall not be called"), () => true)
            );

            var ex = Assert.Throws<Exception>(() => new List<KeyValuePair<int, int>>(dict));
            Assert.Equal("i shall not be called", ex.Message);
        }

        [Fact]
        public void ValuesAreNotBuiltWhenLazy()
        {
            var dict = new LazyDict<int, int>(true,
                new FkKvp<int, int>(() => 1, () => throw new Exception("i shall not be called"), () => true)
            );

            var ex = Assert.Throws<InvalidOperationException>(() => dict.GetEnumerator());
            Assert.Equal("Cannot get the enumerator because this is a lazy dictionary. Enumerating the entries would build all values. If you need this behaviour, set the ctor param 'rejectBuildingAllValues' to false.", ex.Message);
        }

        [Fact]
        public void ValuesAreBuiltWhenRejectionEnabledButValuesNotLazy()
        {
            var dict = new LazyDict<int, int>(true,
               new FkKvp<int, int>(() => 1, () => throw new Exception("i shall not be called"), () => false)
           );

            var ex = Assert.Throws<Exception>(() => new List<KeyValuePair<int, int>>(dict));
            Assert.Equal("i shall not be called", ex.Message);
        }

        [Fact]
        public void CanGetEnumeratorWhenEmpty()
        {
            var dict = new LazyDict<int, int>(true);
            dict.GetEnumerator();
        }
    }
}
