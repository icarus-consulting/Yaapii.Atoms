using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Map;
using Yaapii.Atoms.Number;

namespace Yaapii.Atoms.Map.Tests
{
    public sealed class GroupedTest
    {
        [Fact]
        public void GroupsListToDictionaryOfKeysAndLists()
        {
            var srcList = new ListOf<string>("ABC", "ABCD", "ABCDE");
            var keyFunc =
                new FuncOf<string, double>((str) =>
                    new NumberOf(
                        new IO.LengthOf(new InputOf(str)).Value()
                    ).AsDouble()
                );

            var valueFunc =
                new FuncOf<string, string>((str) =>
                    "ica" + str
                );
            Assert.Equal(
                "icaABCD",
                new Grouped<string,double, string>(srcList, keyFunc, valueFunc)[3.0][1]
            );
        }
    }
}
