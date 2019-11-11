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
using Xunit;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Texts.Tests
{
    public sealed class ContainsTest
    {
        [Fact]
        public void FindsStringInString()
        {
            Assert.True(
                new Contains(
                    "Hallo Welt!", 
                    "Welt"
                ).Value() 
            );
        }

        [Fact]
        public void FindsStringInStringIgnoreCase()
        {
            Assert.True(
                new Contains(
                    "Hallo Welt!", 
                    "welt", 
                    true
                ).Value()
            );
        }

        [Fact]
        public void FindsTextInText()
        {
            Assert.True(
                new Contains(
                    new Text.Live("Hallo Welt!"), 
                    new Text.Live("Welt")
                ).Value()
            );
        }

        [Fact]
        public void FindsTextInTextIgnoreCase()
        {
            Assert.True(
                new Contains(
                    new Text.Live("Hallo Welt!"), 
                    new Text.Live("welt"), 
                    true
                ).Value()
            );
        }

        [Fact]
        public void FindsStringScalarInStringScalar()
        {
            Assert.True(
                new Contains(
                    new ScalarOf<string>("Hallo Welt!"), 
                    new ScalarOf<string>("Welt")
                ).Value()
            );
        }

        [Fact]
        public void FindsITextInITextIgnoreCase()
        {
            Assert.True(
                new Contains(
                    new ScalarOf<string>("Hallo Welt!"), 
                    new ScalarOf<string>("welt"), 
                    new ScalarOf<StringComparison>(StringComparison.CurrentCultureIgnoreCase)
                ).Value()
            );
        }

        [Fact]
        public void FindsNotStringInString()
        {
            Assert.False(
                new Contains(
                    "Hallo Welt!", 
                    "welt"
                ).Value()
            );
        }

        [Fact]
        public void FindsNotStringInStringIgnoreCase()
        {
            Assert.False(
                new Contains(
                    "Hallo Welt!", 
                    "world", 
                    true
                ).Value()
            );
        }

        [Fact]
        public void FindsNotTextInText()
        {
            Assert.False(
                new Contains(
                    new Text.Live("Hallo Welt!"), 
                    new Text.Live("welt")
                ).Value()
            );
        }

        [Fact]
        public void FindsNotTextInTextIgnoreCase()
        {
            Assert.False(
                new Contains(
                    new Text.Live("Hallo Welt!"), 
                    new Text.Live("world"), 
                    true
                ).Value()
            );
        }

        [Fact]
        public void FindsNotStringScalarInStringScalar()
        {
            Assert.False(
                new Contains(
                    new ScalarOf<string>("Hallo Welt!"), 
                    new ScalarOf<string>("welt")
                ).Value()
            );
        }

        [Fact]
        public void FindsNotITextInITextIgnoreCase()
        {
            Assert.False(
                new Contains(
                    new ScalarOf<string>("Hallo Welt!"), 
                    new ScalarOf<string>("world"), 
                    new ScalarOf<StringComparison>(
                        StringComparison.CurrentCultureIgnoreCase
                    )
                ).Value()
            );
        }
    }
}
