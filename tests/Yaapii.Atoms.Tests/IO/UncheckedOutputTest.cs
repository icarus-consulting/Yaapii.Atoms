using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.IO;

namespace Yaapii.Atoms.Tests.IO
{
    public sealed class UncheckedOutputTest
    {
        //[Fact]
        //public void RethrowsCheckedToUncheckedException()
        //{
        //    var output = new OutputTo(new MemoryStream());
        //    output.Dispose();

        //    Assert.Throws(
        //        typeof(UncheckedIOException),
        //        () => new UncheckedOutput(
        //                output
        //                ).Stream().Length);
        //}
    }
}