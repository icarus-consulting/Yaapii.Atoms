using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Yaapii.Atoms.Lookup.Tests
{
    public sealed class LazyDictTests
    {
        [Fact]
        public void ValuesAreBuilt()
        {
            var dict = new LazyDict<int, int>(false,
                new FailingValueKvp(1)
            );

            var ex = Assert.Throws<Exception>(() => new List<KeyValuePair<int, int>>(dict));
            Assert.Equal("i shall not be called", ex.Message);
        }

        [Fact]
        public void ValuesAreNotBuilt()
        {
            var dict = new LazyDict<int, int>(true,
                new FailingValueKvp(1)
            );

            var ex = Assert.Throws<InvalidOperationException>(() => dict.GetEnumerator());
            Assert.Equal("Cannot get the enumerator because this is a lazy dictionary. Enumerating the entries would build all values. If you need this behaviour, set the ctor param 'rejectBuildingAllValues' to false.", ex.Message);
        }

        /// <summary>
        /// IKvp<int, int> with an always failing Value()
        /// </summary>
        private sealed class FailingValueKvp : IKvp<int, int>
        {
            private readonly int key;

            /// <summary>
            /// IKvp<int, int> with an always failing Value()
            /// </summary>
            public FailingValueKvp(int key)
            {
                this.key = key;
            }

            public int Value()
            {
                throw new Exception("i shall not be called");
            }

            public int Key()
            {
                return key;
            }

            public bool IsLazy()
            {
                return true;
            }
        }
    }
}
