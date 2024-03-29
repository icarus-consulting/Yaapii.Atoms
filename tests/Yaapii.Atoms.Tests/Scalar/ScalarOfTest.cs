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
using System.Diagnostics;
using Xunit;

namespace Yaapii.Atoms.Scalar.Tests
{
    public sealed class ScalarOfTest
    {
        [Fact]
        public void CachesScalarResults()
        {
            IScalar<int> scalar =
                new ScalarOf<int>(
                    () => new Random().Next());

            var val1 = scalar.Value();
            System.Threading.Thread.Sleep(2);

            Assert.True(val1 == scalar.Value(),
                "cannot return value from cache"
            );
        }

        [Fact]
        public void ReloadCachedScalarResults()
        {
            IScalar<List<int>> scalar =
                new ScalarOf<List<int>>(
                    () => new List<int>() { new Random().Next() },
                    lst => lst.Count > 1);

            var lst1 = scalar.Value();
            System.Threading.Thread.Sleep(2);

            Assert.True(lst1.GetHashCode() == scalar.Value().GetHashCode(), "cannot return value from cache");
            lst1.Add(42);

            Assert.False(lst1.GetHashCode() == scalar.Value().GetHashCode(), "reload doesn't work");
        }
    }
}
