using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Yaapii.Atoms.Enumerable
{
    public class PartitionedTests
    {
        [Fact]
        public void NewPartitionTest()
        {                  
            Assert.Equal(
                2,
                new LengthOf(
                    new Partitioned<string>(
                        1, 
                        new List<string>() { "hokus", "pokus" }
                    )
                ).Value()
            );
        }
    }
}
