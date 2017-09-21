using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
namespace Yaapii.Atoms.Tests.List
{
    public sealed class ItemAtEnumeratorTest
    {
        [Fact]
        public void FirstElementTest()
        {
            Assert.True(
            new ItemAtEnumerator<int>(
                new EnumerableOf<int>(1, 2, 3).GetEnumerator()
            ).Value() == 1,
            "Can't take the first item from the enumerator");
        }

        [Fact]
        public void ElementByPosTest()
        {
            Assert.True(
                new ItemAtEnumerator<int>(
                    new EnumerableOf<int>(1, 2, 3).GetEnumerator(),
                    1
                ).Value() == 2,
                "Can't take the item by position from the enumerator");
        }

        [Fact]
        public void FailForEmptyCollectionTest()
        {
            Assert.Throws(
                typeof(IOException),
                () => new ItemAtEnumerator<int>(
                        new EnumerableOf<int>(new int[0]).GetEnumerator(),
                        0
                        ).Value());
        }

        [Fact]
        public void FailForNegativePositionTest()
        {
            Assert.Throws(
                typeof(IOException),
                    () => new ItemAtEnumerator<int>(
                        new EnumerableOf<int>(1, 2, 3).GetEnumerator(),
                        -1
            ).Value());
        }

        [Fact]
        public void FallbackTest()
        {
            string fallback = "fallback";

            Assert.True(
            new ItemAtEnumerator<string>(
                new EnumerableOf<string>().GetEnumerator(),
                fallback
            ).Value() == fallback,
            "Can't fallback to default value");
        }

        [Fact]
        public void FailForPosMoreLengthTest()
        {
            Assert.Throws(
                typeof(IOException), () => 
                new ItemAtEnumerator<int>(
                    new EnumerableOf<int>(1, 2, 3).GetEnumerator(),
                3
            ).Value());
        }
    }
}
#pragma warning restore MaxPublicMethodCount // a public methods count maximum
