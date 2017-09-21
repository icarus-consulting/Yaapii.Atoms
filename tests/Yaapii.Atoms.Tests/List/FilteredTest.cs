using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Func;
using System.Linq;

namespace Yaapii.Atoms.Tests.List
{
    public sealed class FilteredTests
    {
        [Fact]
        public void Filters()
        {
            Assert.True(
                new LengthOf<string>(
                    new Filtered<string>(
                        new List<string>() { "A", "B", "C" },
                        new FuncOf<string, bool>(
                            (input) => input != "B"))).Value() == 2,
                "cannot filter items");
        }

        [Fact]
        public void FiltersEmptyList()
        {
            Assert.True(
                new LengthOf<string>(
                    new Filtered<string>(
                        new EnumerableOf<String>(),
                        input => input.Length > 1)
                    ).Value() == 0,
                "cannot filter empty enumerable");
        }

        [Fact]
        public void PerformanceMatchesLinQ()
        {
            Func<string,bool> filter = (input) => input != "B";

            var linq = new ElapsedTime(() => new List<string>() { "A", "B", "C" }.Where(filter)).AsTimeSpan();
            var atoms =
                new ElapsedTime(
                    () => new Filtered<string>(
                        new List<string>() { "A", "B", "C" },
                            filter)).AsTimeSpan();

            Assert.True((linq - atoms).Duration().Milliseconds < 10);
        }
    }
}
