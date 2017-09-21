using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.IO
{
    public sealed class WriterToTest
    {
        [Fact]
        public void WritesContentToFile()
        {
            var dir = "artifacts/WriterToTest"; var file = "txt.txt";
            var uri = new Uri(Path.GetFullPath(Path.Combine(dir, file)));
            Directory.CreateDirectory(dir);
            var content = "yada yada";

            string s = "";
            using (var ipt =
                new TeeInput(
                    new InputOf(content),
                    new WriterAsOutput(
                        new WriterTo(uri))))
            {
                s = new TextOf(ipt).AsString();
            }

            Assert.True(
                Encoding.UTF8.GetString(
                    new InputAsBytes(
                        new InputOf(uri)
                    ).AsBytes()).CompareTo(s) == 0, //.Equals is needed because Streamwriter writes UTF8 _with_ BOM, which results in a different encoding.
            "Can't copy Input to Output and return Input");
        }
    }
}
