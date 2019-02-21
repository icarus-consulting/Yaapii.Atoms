using System;
using System.Collections.Generic;
using System.Text;
using Xunit;


namespace Yaapii.Atoms.IO.Tests
{
    public class ZipMappedPathsTests
    {
        [Fact]
        public void Maps()
        {
            Assert.Contains<string>(
                "directory/File1",
                new ZipFiles(
                    new ZipMappedPaths(
                        path => "directory/" + path,
                        new ResourceOf(
                            "Assets/Zip/ZipWithThreeFiles.zip",
                            this.GetType()
                        )
                    )             
                )
            );
        }
    }
}
