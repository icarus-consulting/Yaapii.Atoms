﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Tests.Enumerable
{
    public class ArrayListAsEnumerableTest
    {
        [Fact]
        public void BuildsFromStrings()
        {
            var arr = new ArrayList() { "A", "B", "C" };

            Assert.True(
                new ItemAt<object>(
                    new ArrayListAsEnumerable(arr)
                ).Value().ToString() == "A");
        }
    }
}